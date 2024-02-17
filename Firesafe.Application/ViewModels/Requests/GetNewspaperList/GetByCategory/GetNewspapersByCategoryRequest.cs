namespace Application.ViewModels.Requests.GetNewspaperList.GetByCategory;

public class GetNewspapersByCategoryRequest : BaseRequestModel
{
    public int Page { get; init; } = 1;
}