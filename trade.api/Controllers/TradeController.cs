using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trade.api.Models.DTOs.TradeDTOs;
using trade.api.Services;

namespace trade.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var result = _tradeService.NewTrade(tradePostDto);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
