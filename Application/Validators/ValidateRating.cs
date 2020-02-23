using FluentValidation;

namespace Application.Validators
{
    public static class ValidateRating
    {
        public static IRuleBuilder<T, int> Rating<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            var options = ruleBuilder
                        .GreaterThanOrEqualTo(1)
                        .LessThanOrEqualTo(5)
                        .WithMessage("Rating should be within 1 - 5, 1 is the lowest and 5 is the highest");
            
            return options;
        }
    }
}