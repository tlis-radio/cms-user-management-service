using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using Tlis.Cms.UserManagement.Api.RpcConsumers.Base;
using Tlis.Cms.UserManagement.Application.Contracts.Rpc.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Rpc.Responses;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Api.RpcConsumers;

internal class GetUsersNickNameRequestConsumer(IServiceProvider serviceProvider, ObjectPool<IModel> channel)
    : BaseRpcConsumer<GetUsersNickNameRequest, GetUsersNickNameResponse>(serviceProvider, "user-management-service-rpc:get-users-nickname", channel)
{
    protected override async Task<GetUsersNickNameResponse> ProcessMessage(GetUsersNickNameRequest request, IUnitOfWork unitOfWork)
    {
        var result = await unitOfWork.UserRepository.GetUsersWithOnlyNickName(request.Ids);

        return new GetUsersNickNameResponse
        {
            Results = result
                .Select(r => new GetUsersNickNameResponseResult
                {
                    Id = r.Id,
                    NickName = r.Nickname
                }).ToList()
        };
    }
}