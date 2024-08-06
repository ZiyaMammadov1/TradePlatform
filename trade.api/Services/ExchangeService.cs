using Microsoft.Extensions.Caching.Memory;
using trade.api.Models.Common;
using trade.api.Models.DTOs.ExchnageDTOs;
using trade.api.Models.DTOs.IndicatorDTOs;

namespace trade.api.Services
{
    public class ExchangeService
    {
        private decimal upperlimit = 1.099235M;
        private decimal lowerlimit = 0;
        private readonly Random _random;
        private readonly IMemoryCache _memoryCache;
        private Queue<decimal> _lastRates = new();
        private IndicatorService _indicatorService;

        public ExchangeService(IMemoryCache memoryCache, IndicatorService indicatorService)
        {
            _random = new Random();
            _memoryCache = memoryCache;
            _indicatorService = indicatorService;
        }
        public ApiResponse GetFakeCurrentExchangeRateAndIndicatorValues(IndicatorPostDto indicators = null)
        {
            lowerlimit = DateTime.Now.Second switch
            {
                var second when second >= 0 && second <= 10 => 1.099105M,
                var second when second >= 10 && second <= 20 => 1.0994505M,
                var second when second >= 20 && second <= 30 => 1.099305M,
                var second when second >= 30 && second <= 40 => 1.099505M,
                var second when second >= 40 && second <= 50 => 1.0999905M,
                var second when second >= 50 && second <= 59 => 1.099515M
            };

            decimal scaled = (decimal)(_random.NextDouble()) * (decimal)(upperlimit - lowerlimit);

            decimal rate = lowerlimit + scaled;

            CheckAndSetRateIntoQueue(rate);

            _memoryCache.Set("lastRates", _lastRates);

            IndicatorValues indicatorValues = _indicatorService.CalculateValues(indicators, rate);

            ExchangeGetDto exchangeGetDto = new ExchangeGetDto
            {
                Rate = rate,
                IndicatorValues = indicatorValues
            };

            return new ApiResponse() { Data = exchangeGetDto, Message = null, Success = true };
        }

        private void CheckAndSetRateIntoQueue(decimal scaled)
        {
            if (_lastRates.Count == 10) _lastRates.Dequeue();

            _lastRates.Enqueue(scaled);

        }

    }
}
