using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EFCore.Extensions
{
    public static class PagedListExtensions
    {   //Mesajlaşma için bir PagedList metot
        // return await messages.ToPagedListAsync(pageNumber, pageSize);
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync(); //Sql'den toplam veri sayısını bulur.
            var items = await source // ihtiyacımız veri kadarını bize geri döner
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
