using Firesafe.Domain.Entities;
using FluentValidation;

namespace Application.ViewModels.Requests.SetRole;

public class SetRoleValidator : AbstractValidator<SetRolesRequest>
{
    public SetRoleValidator()
    {
        RuleFor(x => x.Roles).Must(BeAValidRoles)
            .WithMessage("Role {PropertyValue} is not valid!");
    }

    private bool BeAValidRoles(List<string> roles)
    {
        var validRoles = Roles.GetAll();
        return roles.All(r => validRoles.Contains(r));
    }
}