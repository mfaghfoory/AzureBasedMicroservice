using AzureBasedMicroservice.EntityFramework.Alterings;
using FluentValidation;

namespace AlteringsRegistrationService.Validators
{
    public class NewAlterationValidator : AbstractValidator<Altering>
    {
        public NewAlterationValidator()
        {
            RuleFor(x => x.Value).NotEqual(0);
            RuleFor(x => x.Value).LessThanOrEqualTo(5);
            RuleFor(x => x.Value).GreaterThanOrEqualTo(-5);
        }
    }
}
