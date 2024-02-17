using Application.Services.Interface;
using Firesafe.Domain.Commands;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Domain.Entities;
using Firesafe.Domain.UnitOfWork;

namespace Application.Services.Impl;

public class RoleService(IUnitOfWork uow, IMediatorHandler mediatorHandler) : BaseService, IRoleService
{
    public IEnumerable<string> GetAllRoles()
    {
        return uow.RoleRepository.GetAll().Select(r => r.Type);
    }

    public void SetRole(Guid userId, List<string> roles)
    {
        mediatorHandler.SendCommand(new SetUserRolesCommand
        {
            UserId = userId,
            Roles = roles.Select(r => r.Trim()).ToList()
        });
    }
}