using LeaveManagement_Models.DataContext;
using LeaveManagement_Models.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Models.Repository
{
    public class genericRepository<T> : IgenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbset;
        private readonly LeaveManagementDataContext _context;
        public genericRepository(LeaveManagementDataContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }
        public async Task<T> AddItem(T entity)
        {
            _dbset.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteItem(T entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbset;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetData()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<T> GetOneId(int id)
        {
            return await _dbset.FindAsync(id);
        }        

        public async Task UpdateItem(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
