using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Requests.DeleteNewspaper;

public class DeleteNewspaperRequest : BaseRequestModel
{
    [Required] public required Guid Id { get; init; }
}