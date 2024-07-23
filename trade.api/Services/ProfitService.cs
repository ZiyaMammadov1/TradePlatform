namespace trade.api.Services
{
    public class ProfitService
    {
        public double CalculateProfit(double invest, bool isWon)
        {
            if (isWon)
            {
                return invest * 0.95;
            }
            return -invest;
        }
    }
}
