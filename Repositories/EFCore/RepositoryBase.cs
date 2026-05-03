using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    //bool trackChanges => Üzerinde değişiklik yapacaksan, efcore bunun takibini yapması için true demelisin. Ama eğer sadece veri listeleteceksen bunu takip etme diyebilirsin.
    public class RepositoryBase<T> : IRepositoriesBase<T> where T : class
    {
        protected readonly RepositoriesContext _context; //Kalıtımla bir sonraki Repolarda da erişilebilmesini sağladık. Böylece doğrudan bunu kullanarak veritabanı işlemlerini yapabilir kıldık.

        public RepositoryBase(RepositoriesContext context)
        {
            _context = context;
        }

        public void Create(T entity) => _context.Set<T>().Add(entity);


        public void Delete(T entity) => _context.Set<T>().Remove(entity);


        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
            _context.Set<T>().AsNoTracking() : //Takip etme
            _context.Set<T>(); // Takip et


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> exception, bool trackChanges) =>
            !trackChanges ?
            _context.Set<T>().Where(exception).AsNoTracking() : //Takip etme
            _context.Set<T>().Where(exception); // Takip et

        public void Update(T entity) => _context.Set<T>().Update(entity);

    }
}
