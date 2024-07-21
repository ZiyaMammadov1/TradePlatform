using trade.api.DTOs.TradeDTOs;

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

        public double NewTrade(TradePostDto tradePostDto)
        {
            bool isWon = false;
            decimal previousRate = _exchangeService.GetCurrentExchangeRateValue();

            Task.Delay(tradePostDto.TimeSpan);

            decimal nextRate = _exchangeService.GetCurrentExchangeRateValue();

            if ((tradePostDto.Direction && nextRate > previousRate) || (!tradePostDto.Direction && nextRate < previousRate))
            {
                isWon = true;
            }

            return _profitService.CalculateProfit(tradePostDto.Invest, isWon);
        }
    }
}
