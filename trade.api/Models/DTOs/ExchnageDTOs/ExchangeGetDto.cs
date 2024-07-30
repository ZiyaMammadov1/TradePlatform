using trade.api.Models.DTOs.IndicatorDTOs;

namespace trade.api.Models.DTOs.ExchnageDTOs
{
    public class ExchangeGetDto
    {
        public decimal Rate { get; set; }
        public IndicatorValues IndicatorValues { get; set; }
    }
}
