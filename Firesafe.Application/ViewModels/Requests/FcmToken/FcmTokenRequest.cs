namespace Application.ViewModels.Requests.FcmToken;

public class FcmTokenRequest : BaseRequestModel
{
    public required string Token { get; init; }
}