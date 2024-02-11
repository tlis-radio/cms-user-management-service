using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserRoleHistoryUpdateRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UserRoleHistoryUpdateRequest, bool>
{
    public async Task<bool> Handle(UserRoleHistoryUpdateRequest request, CancellationToken cancellationToken)
    {
        var userRoleHistory = await unitOfWork.UserRoleHistoryRepository.GetByIdAsync(request.HistoryId, asTracking: true);
        if (userRoleHistory is null || userRoleHistory.UserId != request.Id)
        {
            return false;
        }

        userRoleHistory.FunctionStartDate = request.FunctionStartDate;
        userRoleHistory.FunctionEndDate = request.FunctionEndDate;
        userRoleHistory.Description = request.Description;

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}