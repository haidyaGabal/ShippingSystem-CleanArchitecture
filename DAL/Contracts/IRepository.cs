using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{

    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(Guid id);
        bool Add(T entity);
        bool Add(T entity, out Guid id);
        bool Update(T entity);
        bool Delete(Guid id);
        bool ChangeStatus(Guid id, Guid userId, int status = 1);
        T FirstOrDefault(Expression<Func<T, bool>> filter);
        Task<List<T>> GetList(Expression<Func<T, bool>> filter);
        Task<List<T>> GetList(
            Expression<Func<T, bool>> filter,
            params Expression<Func<T, object>>[] includes);

        // NEW: paginated overload
        Task<PagedResult<T>> GetList(
            Expression<Func<T, bool>> filter,
            PaginationParams pagination,
            params Expression<Func<T, object>>[] includes);
    }
}
