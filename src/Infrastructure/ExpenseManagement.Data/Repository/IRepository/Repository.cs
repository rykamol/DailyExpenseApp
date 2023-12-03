using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Data.Repository.IRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ExpenseDbContext _context;
        internal DbSet<T> dbSet;

        public Repository(ExpenseDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public void Create(T entity)
        {
            try
            {
                dbSet.Add(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                dbSet.Remove(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            try
            {
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includeProperties != null)
                {
                    foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(property);

                    }
                }

                return query;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            try
            {
                query = query.Where(filter);

                if (includeProperties != null)
                {
                    foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(property);

                    }
                }
                return query.Where(filter).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                dbSet.RemoveRange(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
