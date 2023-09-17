using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Tlis.Cms.UserManagement.Infrastructure.Configurations;

namespace Tlis.Cms.UserManagement.Infrastructure.PooledObjects;

internal class RabbitMqModelPooledObject : IPooledObjectPolicy<IModel>
{
    private IConnection Connection { get; }

    public RabbitMqModelPooledObject(IOptions<RabbitMqConfiguration> configuration)
    {
        var factory = new ConnectionFactory {
            HostName = configuration.Value.HostName,
            UserName = configuration.Value.UserName,
            Password = configuration.Value.Password,
            DispatchConsumersAsync = true
        };
        Connection = factory.CreateConnection();
    }

    public IModel Create()
    {
        return Connection.CreateModel();  
    }

    public bool Return(IModel obj)
    {
        if (obj.IsOpen)  
        {  
            return true;  
        }  
        else  
        {  
            obj?.Dispose();  
            return false;  
        }  
    }
}