using Microsoft.AspNetCore.Builder;
using trade.api.Services;

namespace trade.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRouting(op => { op.LowercaseUrls = true; });


            builder.Services.AddScoped<ExchangeService>();
            builder.Services.AddScoped<TradeService>();
            builder.Services.AddScoped<ProfitService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("current-rate", (ExchangeService exchangeService) => exchangeService.GetCurrentExchangeRateValue());

            app.MapControllers();

            app.Run();
        }
    }
}
