using trade.api.Models.DTOs.IndicatorDTOs;

namespace trade.api.Services
{
    public class IndicatorService
    {
        private int count = 0;
        public IndicatorValues CalculateValues(IndicatorPostDto indicators)
        {
            IndicatorValues values = new IndicatorValues();

            count++;

            values.MA = 0;

            return values;
        }

    }
}
