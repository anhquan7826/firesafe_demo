using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Requests.SetRole;

public class SetRolesRequest : BaseRequestModel
{
    [Required] public List<string> Roles { get; init; } = new();
}