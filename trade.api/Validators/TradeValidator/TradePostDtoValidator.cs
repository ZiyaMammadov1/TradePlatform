using FluentValidation;
using trade.api.Models.DTOs.TradeDTOs;

namespace trade.api.Validators.TradeValidator
{
    public class TradePostDtoValidator : AbstractValidator<TradePostDto>
    {
        public TradePostDtoValidator()
        {
            RuleFor(x => x.Invest).NotNull().NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(5000);
            RuleFor(x => x.CurrentRate).NotNull().NotEmpty().GreaterThan(0).LessThan(2);
            RuleFor(x => x.TimeSpan).NotNull().NotEmpty().GreaterThanOrEqualTo(new TimeSpan(0, 0, 05)).LessThanOrEqualTo(new TimeSpan(24, 0, 0));
        }
    }
}
