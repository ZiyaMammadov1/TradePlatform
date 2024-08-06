using trade.api.Models.DTOs.IndicatorDTOs;

namespace trade.api.Services
{
    public class IndicatorService
    {
        private readonly Random _random = new Random();
        public IndicatorValues CalculateValues(IndicatorPostDto indicators, decimal fakeRate)
        {
            IndicatorValues values = new IndicatorValues();

            if (indicators.MA) values.MA = GenerateFakeIndicatorValue(fakeRate);

            return values;
        }

        private decimal GenerateFakeIndicatorValue(decimal fake)
        {
            return (decimal)_random.NextDouble();
        }

    }
}
