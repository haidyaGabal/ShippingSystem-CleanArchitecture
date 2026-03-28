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
        T? GetById(Guid id);
        List<T> GetAll();
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(Guid id);
        Task<bool> ChangeStatus(Guid id,int status = 1);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
    }
}
