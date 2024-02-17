namespace Application.ViewModels.Requests.GetNewspaperList.GetNew;

public class GetNewNewspapersRequest : BaseRequestModel
{
    public int Page { get; init; } = 1;
}