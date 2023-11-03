using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using WebAPI.Middleware;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<SmartShopContext>(options =>
            {
                options.UseSqlServer("name=ConnectionStrings:SmartShop");
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition("X-API-KEY", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Name = "X-API-KEY",
                    Type= Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "ApiKey must appear in header"
                });

                x.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "X-API-KEY"
                            },
                            In = ParameterLocation.Header
                        },
                        new string[]{ }
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ApiKeyMiddleware>();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}