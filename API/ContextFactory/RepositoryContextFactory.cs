using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.Contracts;

namespace API.ContextFactory
{
    /* 
       IDesignTimeDbContextFactory: EF Core araçlarına (Migration/Update-Database) 
       bir "yol haritası" sunar. Terminale komut yazdığında bu sınıf otomatik devreye girer.
    */
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoriesContext>
    {
        public RepositoriesContext CreateDbContext(string[] args)
        {
            // 1. ADIM: appsettings.json DOSYASINA ULAŞMAK
            // Uygulama o an çalışmadığı için ayar dosyasını elimizle (manuel) okuyoruz.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // API projesinin klasörünü baz al
                .AddJsonFile("appsettings.json") // Bağlantı cümlesini buradan al
                .Build();

            // 2. ADIM: VERİTABANI AYARLARINI YAPILANDIRMAK
            // DbContext'in nasıl çalışacağını tarif ediyoruz.
            var builder = new DbContextOptionsBuilder<RepositoriesContext>()
                .UseSqlServer(configuration.GetConnectionString("sqlConnection"), // Bağlantı adresini kullan

                // CRITICAL: Migration dosyalarının hangi projede oluşacağını seçiyoruz.
                // Eğer projenin adı "API" ise burası "API" kalmalı.
                prj => prj.MigrationsAssembly("API"));

            // 3. ADIM: CONTEXT'İ OLUŞTURUP GERİ DÖNMEK
            // EF Core bu oluşturulan nesne üzerinden veritabanı tablolarını hazırlar.
            return new RepositoriesContext(builder.Options);
        }
    }
}