namespace Application.ViewModels.Requests.GetNewspaperList.GetByField;

public class GetNewspapersByFieldRequest : BaseRequestModel
{
    public int Page { get; init; } = 1;
}