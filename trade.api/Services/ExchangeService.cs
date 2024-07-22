using Microsoft.AspNetCore.Identity;

namespace trade.api.Services
{
    public class ExchangeService
    {
        private decimal upperlimit = 1.099235M;
        private decimal lowerlimit = 0;
        private readonly Random random;

        public ExchangeService()
        {
            random = new Random();
        }
        public decimal GetCurrentExchangeRateValue()
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

            decimal scaled = (decimal)(random.NextDouble()) * (decimal)(upperlimit - lowerlimit);
            return lowerlimit + scaled;
        }

    }
}
