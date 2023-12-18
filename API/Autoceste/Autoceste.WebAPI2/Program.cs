using Autoceste.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace Autoceste.WebAPI2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // ignore nulls in serializing response
            builder.Services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Autoceste API",
                    Version = "v1",
                    Contact = new OpenApiContact { Email = "petar.novak@fer.hr" },
                    License = new OpenApiLicense { Name = "CC0 1.0 Universal", Url = new Uri("https://creativecommons.org/publicdomain/zero/1.0/deed.en") }
                });
            });

            // dependency inj for database
            builder.Services.AddDbContext<OrContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("OrDatabaseConnection")));

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}