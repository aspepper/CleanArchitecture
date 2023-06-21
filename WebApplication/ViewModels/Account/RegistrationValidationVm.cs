using FluentValidation;

namespace AdviceCompliance.WebApplication.ViewModels.Account
{
    public class RegistrationValidationVm : AbstractValidator<RegistrationVM>
    {
        public RegistrationValidationVm()
        {
            RuleFor(_ => _.FirstName).NotEmpty();
            RuleFor(_ => _.LastName).NotEmpty();
            RuleFor(_ => _.Email).EmailAddress().NotEmpty();
            RuleFor(_ => _.Password).NotEmpty().WithMessage("Sua senha não pode estar vazia.")
                    .MinimumLength(3).WithMessage("O tamanho da sua senha deve ser de pelo menos 3.")
                    .MaximumLength(20).WithMessage("O tamanho da sua senha não deve exceder 20.")
                    .Matches(@"[A-Z]+").WithMessage("Sua senha deve conter pelo menos uma letra maiúscula.")
                    .Matches(@"[a-z]+").WithMessage("Sua senha deve conter pelo menos uma letra minúscula.")
                    .Matches(@"[0-9]+").WithMessage("Sua senha deve conter pelo menos um número.")
                    .Matches(@"[\@\!\?\*\.]+").WithMessage("Sua senha deve conter pelo menos um (@!? *.).");

            /*
             
            RuleFor(_ => _.Password).NotEmpty().WithMessage("Your password cannot be empty.")
                    .MinimumLength(3).WithMessage("Your password length must be at least 3.")
                    .MaximumLength(20).WithMessage("Your password length must not exceed 20.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\@\!\?\*\.]+").WithMessage("Your password must contain at least one (@!? *.).");
             
             */

            RuleFor(_ => _.ConfirmPassword).Equal(_ => _.Password).WithMessage("ConfirmPassword must equal Password");
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<RegistrationVM>.CreateWithOptions((RegistrationVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid) { return Array.Empty<string>(); }
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }

}
