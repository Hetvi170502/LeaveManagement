using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Models.IRepository
{
    public interface IgenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetData();
        Task<T> GetOneId(int id);
        Task<T> AddItem(T entity);
        Task UpdateItem(T entity);
        Task DeleteItem(T entity);
    }
}
