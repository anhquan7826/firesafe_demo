using FluentValidation;

namespace Application.ViewModels.Requests.FcmToken;

public class FcmTokenValidator : AbstractValidator<FcmTokenRequest>
{
    public FcmTokenValidator()
    {
        RuleFor(x => x.Token).NotNull().NotEmpty().WithMessage("Invalid FCM Token!");
    }
}