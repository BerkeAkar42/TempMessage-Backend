
using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.EFCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.AddAutoMapper(typeof(Program));
            //---------------------------------------------------------------//


            var app = builder.Build();

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
