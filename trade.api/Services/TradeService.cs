using Microsoft.Extensions.Caching.Memory;
using trade.api.Models.Common;
using trade.api.Models.DTOs.ProfitDTOs;
using trade.api.Models.DTOs.TradeDTOs;
using trade.api.Models.Entities;

namespace trade.api.Services
{
    public class TradeService
    {
        public readonly ExchangeService _exchangeService;
        public readonly ProfitService _profitService;
        public readonly UserService _userService;
        public readonly IMemoryCache _memoryCache;

        public TradeService(ExchangeService exchangeService, ProfitService profitService, UserService userService, IMemoryCache memoryCache)
        {
            _exchangeService = exchangeService;
            _profitService = profitService;
            _userService = userService;
            _memoryCache = memoryCache;
        }

        public ApiResponse<ProfitGetDto> NewTrade(TradePostDto tradePostDto)
        {
            ApiResponse<bool> verify = _userService.CheckBudge(tradePostDto.Invest);

            if (verify.Success)
            {
                bool isWon = false;
                decimal previousRate = (decimal)_exchangeService.GetCurrentExchangeRateAndIndicatorValues().Data;

                Thread.Sleep(tradePostDto.TimeSpan);

                decimal nextRate = (decimal)_exchangeService.GetCurrentExchangeRateAndIndicatorValues().Data;

                if ((tradePostDto.Direction && nextRate > previousRate) || (!tradePostDto.Direction && nextRate < previousRate))
                {
                    isWon = true;
                }

                double profit = _profitService.CalculateProfit(tradePostDto.Invest, isWon);
                double deposit = (double)verify.Data + profit;

                _memoryCache.Set("deposit", deposit);

                return new ApiResponse<ProfitGetDto>()
                {
                    Data = new ProfitGetDto()
                    {
                        Deposit = deposit,Earn = profit
                    }, 
                    Success = true, Message = null
                };

            }

            return new ApiResponse<ProfitGetDto>()
            {
                Data = null, Success = true, Message = verify.Message
            };

        }
    }
}
