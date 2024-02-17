using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Requests.GetNewspaper;

public class GetNewspaperRequest : BaseRequestModel
{
    [Required] public required Guid Id { get; init; }
}