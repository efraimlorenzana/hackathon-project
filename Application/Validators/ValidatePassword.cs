using FluentValidation;

namespace Application.Validators
{
    public static class ValidatePassword
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                        .NotEmpty()
                        .MinimumLength(6).WithMessage("Password must be at least 6 character")
                        .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                        .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                        .Matches("[0-9]").WithMessage("Password must contain at least one number")
                        .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non-alphanumeric character");
            
            return options;
        }
    }
}