using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using System.Reflection;
using System.Text;

namespace API.Extensions
{
    public static class ServicesExtensions //Burası program.cs'i servis kayıtlarıyla kirletmemek için, uzun kodları buraya yazdığımız bir classtır.
    {
        //Hangi sınıfı genişletiyorsak "this" ile o parametreyi veriyoruz.
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoriesContext>(options => options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }


        //this olduğu için o parametreyi vermek zorunda değiliz.
        // Repo imzalarının bulunduğu interface tanımlaamsı
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            //DI kaydı. Biri senden IRepositoryManager isterse sen ona RepositoryManager dön.
            //RepositoryManager da kendi içinde sqlConnectionString'ini barındırdığı için o da bir üstteki metotu çağırıyor. Böylede sql bağlantımız gerçekleşiyor.
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        // Servis imzalarının bulunduğu interface tanımlaması
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerService, LoggerManager>();
        }


        public static void ConfigureJWTService(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. JWT Ayarlarını Al
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"];

            // 2. Authentication Servisini Ekle
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TempMessage API",
                    Version = "v1",
                    Description = "TempMessage Backend Projesi Swagger Dokümantasyonu"
                });

                // XML Yorumlarını Swagger'a dahil etme (Opsiyonel: Eğer Controller'daki /// yorumları görmek istersen)
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    s.IncludeXmlComments(xmlPath);
                }

                // 1. JWT Güvenlik Tanımı
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Lütfen başına 'Bearer ' yazarak token'ı giriniz. Örn: 'Bearer eyJhbGci...'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                // 2. Kilit Simgesini Tüm Metotlara Uygula
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            });
        }

    }
}
