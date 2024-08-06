using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text;
using trade.api.Models.Common;
using trade.api.Models.DTOs.IndicatorDTOs;
using trade.api.Services;

namespace trade.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var modelState = actionContext.ModelState.Values;
                        return new BadRequestObjectResult(new ApiResponse()
                        {
                            Data = "",
                            Success = false,
                            Message = modelState.SelectMany(x => x.Errors, (x, y) => y.ErrorMessage).ToList()
                        });
                    };
                });


            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("az");

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(op =>
            {
                op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                op.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

                op.SwaggerDoc("v1", new OpenApiInfo { Title = "TradePlatform", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                op.IncludeXmlComments(xmlPath);
            });
            builder.Services.AddAuthorization();
            builder.Services.AddRouting(op => { op.LowercaseUrls = true; });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            builder.Services.AddMemoryCache();

            builder.Services.AddScoped<ExchangeService>();
            builder.Services.AddScoped<TradeService>();
            builder.Services.AddScoped<ProfitService>();
            builder.Services.AddScoped<LoginService>();
            builder.Services.AddScoped<JwtService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IndicatorService>();

            builder.Services.AddCors(cors =>
            {
                cors.AddPolicy("corsConfig", conf =>
                {
                    conf.AllowAnyHeader();
                    conf.AllowAnyMethod();
                    conf.AllowAnyOrigin();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
               
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapPost("current-rate", (ExchangeService exchangeService, [FromBody] IndicatorPostDto indicators) => exchangeService.GetFakeCurrentExchangeRateAndIndicatorValues(indicators));

            app.MapControllers();

            app.Run();
        }
    }
}
