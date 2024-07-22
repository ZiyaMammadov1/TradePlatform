namespace trade.api.Models.DTOs.TradeDTOs
{
    public class TradePostDto
    {
        public decimal CurrentRate { get; set; }
        public double Invest { get; set; }
        public bool Direction { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}
