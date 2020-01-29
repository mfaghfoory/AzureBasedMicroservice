using FluentValidation;
using PaymentService.Models;

namespace PaymentService.Validators
{
    public class NewPaymentValidator : AbstractValidator<NewPaymentViewModel>
    {
        public NewPaymentValidator()
        {
            RuleFor(x => x.AlteringId).GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
