
using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog;
using Repositories.EFCore;
using Services.Contracts;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //NLOG Kurulumu
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //------------------------------------
            //Authentication => Kullanýcýnýn kim olduðunu bulur (örn JWT, cookie, header vs.)
            //Authorization => Bulunan kullanýcýnýn bu kaynaða eriþme hakký var mý?, onu kontrol eder
            //------------------------------------

            //cors iþlemleri => tarayýcýnýn güvenlik önlemi ötürüsü bu adrese gelen isteklere izin verdik.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalHost", policy =>
                {
                    policy.WithOrigins("http://localhost:5000")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });



            //Servis kayýtlarý
            //---------------------------------------------------------------//
            builder.Services.ConfigureSqlContext(builder.Configuration); //SQL Connection String
            builder.Services.ConfigureRepositoryManager(); //Repository Manager
            builder.Services.ConfigureServiceManager(); //Service Manager
            builder.Services.AddAutoMapper(typeof(Program)); //Mapper
            builder.Services.ConfigureLoggerService(); //NLOG
            //---------------------------------------------------------------//


            var app = builder.Build();

            //Program.cs de constructor olmadýðý için buradan nesnenin atamasýný yapýyoruz
            var logger = app.Services.GetRequiredService<ILoggerService>();
            app.ConfigureExceptionHandler(logger); //Global Hata Yönetimi

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Cors iþlemine izin verdik.
            app.UseCors("AllowLocalHost");

            app.MapControllers();

            app.Run();
        }
    }
}
