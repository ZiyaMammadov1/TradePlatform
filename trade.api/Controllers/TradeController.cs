using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trade.api.Models.DTOs.TradeDTOs;
using trade.api.Services;

namespace trade.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        public readonly TradeService _tradeService;

        public TradeController(TradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpPost]
        public IActionResult NewTrade(TradePostDto tradePostDto)
        {
            return Ok(_tradeService.NewTrade(tradePostDto));
        }
    }
}
