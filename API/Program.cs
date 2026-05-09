
using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog;
using Repositories.EFCore;
using Services.Contracts;
using System.Reflection;

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
            builder.Services.AddEndpointsApiExplorer(); //Swagger Kurulumu
            builder.Services.AddSwaggerGen(options => //Bu aþama Swagger üzerindeki API'lerin ayrýntýlý bilgilerini vermemize yarayacak.
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            }); //Swagger Kurulumu

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
            builder.Services.ConfigureJWTService(builder.Configuration); //JWT Token Configure
            builder.Services.ConfigureSwagger(); //Swagger Auth Config
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
                app.UseSwagger(); //Swagger Kurulumu
                app.UseSwaggerUI(); //Swagger Kurulumu
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); //"Sen kimsin?"
            app.UseAuthorization(); //"Buraya girmeye yetkin var mý?"

            //Cors iþlemine izin verdik.
            app.UseCors("AllowLocalHost");

            app.MapControllers();
            
            app.Run();
        }
    }
}
