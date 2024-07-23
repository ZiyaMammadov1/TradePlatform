using System.Timers;
using trade.api.Models.Common;
using trade.api.Models.DTOs.TradeDTOs;

namespace trade.api.Services
{
    public class TradeService
    {
        public readonly ExchangeService _exchangeService;
        public readonly ProfitService _profitService;

        public TradeService(ExchangeService exchangeService, ProfitService profitService)
        {
            _exchangeService = exchangeService;
            _profitService = profitService;
        }

        public ApiResponse<double> NewTrade(TradePostDto tradePostDto)
        {
            bool isWon = false;
            decimal previousRate = (decimal)_exchangeService.GetCurrentExchangeRateValue().Data;

            Thread.Sleep(tradePostDto.TimeSpan);

            decimal nextRate = (decimal)_exchangeService.GetCurrentExchangeRateValue().Data;

            if ((tradePostDto.Direction && nextRate > previousRate) || (!tradePostDto.Direction && nextRate < previousRate))
            {
                isWon = true;
            }

            return new ApiResponse<double>()
            {
                Data = _profitService.CalculateProfit(tradePostDto.Invest, isWon),
                Success = true,
                Message = null
            };

        }
    }
}
