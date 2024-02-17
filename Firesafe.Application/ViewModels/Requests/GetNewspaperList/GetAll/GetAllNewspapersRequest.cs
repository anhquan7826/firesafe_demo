namespace Application.ViewModels.Requests.GetNewspaperList.GetAll;

public class GetAllNewspapersRequest : BaseRequestModel
{
    public int Page { get; init; } = 1;
}