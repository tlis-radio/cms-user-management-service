using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Api.RpcConsumers.Base;

internal abstract class BaseRpcConsumer<TRequest, TResponse>(
    IServiceProvider serviceProvider,
    string queueName,
    ObjectPool<IModel> channel) : IHostedService
    where TRequest : class
    where TResponse : class
{
    protected readonly string QueueName = queueName;

    protected readonly IModel Channel = channel.Get();

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Channel.QueueDeclare(
            queue: QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        Channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new AsyncEventingBasicConsumer(Channel);
        Channel.BasicConsume(
            queue: QueueName,
            autoAck: false,
            consumer: consumer);

        consumer.Received += HandleMessage;

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    protected abstract Task<TResponse> ProcessMessage(TRequest request, IUnitOfWork unitOfWork);

    private async Task HandleMessage(object? model, BasicDeliverEventArgs ea)
    {
        var props = ea.BasicProperties;
        var replyProps = Channel.CreateBasicProperties();
        replyProps.CorrelationId = props.CorrelationId;

        try
        {
            var request = JsonSerializer.Deserialize<TRequest>(ea.Body.Span, _jsonSerializerOptions);

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var response = await ProcessMessage(request, scope.ServiceProvider.GetRequiredService<IUnitOfWork>());

                replyProps.ContentType = "application/json";

                SendResponse(response, props.ReplyTo, replyProps);
            }
        }
        catch (Exception e)
        {
            var response = new ProblemDetails
            {
                Title = "An error occurred while processing the request.",
                Status = 500,
                Detail = e.Message
            };

            replyProps.ContentType = "application/problem+json";

            SendResponse(response, props.ReplyTo, replyProps);
        }
        finally
        {
            Channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        }
    }

    private void SendResponse<T>(T response, string replyTo, IBasicProperties replyProps)
        where T : class
    {
        var responseString = JsonSerializer.Serialize(response);
        var responseBytes = Encoding.UTF8.GetBytes(responseString);
        
        Channel.BasicPublish(
            exchange: string.Empty,
            routingKey: replyTo,
            basicProperties: replyProps,
            body: responseBytes);
    }
}