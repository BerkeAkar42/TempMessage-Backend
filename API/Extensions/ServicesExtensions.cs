using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;

namespace API.Extensions
{
    public static class ServicesExtensions //Burası program.cs'i servis kayıtlarıyla kirletmemek için, uzun kodları buraya yazdığımız bir classtır.
    {
        //Hangi sınıfı genişletiyorsak "this" ile o parametreyi veriyoruz.
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoriesContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }


        //this olduğu için o parametreyi vermek zorunda değiliz.
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            //DI kaydı. Biri senden IRepositoryManager isterse sen ona RepositoryManager dön.
            //RepositoryManager da kendi içinde sqlConnectionString'ini barındırdığı için o da bir üstteki metotu çağırıyor. Böylede sql bağlantımız gerçekleşiyor.
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            //services.AddScoped<IServiceManager, ServiceManager>();
        }


    }
}
