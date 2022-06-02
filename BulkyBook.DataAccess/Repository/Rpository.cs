using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Repository
{
    public class Rpository<T>: IRepository<T> where T:class
    {
        public readonly AppDbContext _db;
        internal DbSet<T> dbSet;

        public Rpository(AppDbContext db)
        {
            _db = db;
            //_db.Products.Include(u => u.Category).Include(u=>u.CoverType);
            this.dbSet = _db.Set<T>();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProp = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (includeProp != null)
            {
                foreach (var prop in includeProp.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProp = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProp != null)
            {
                foreach (var prop in includeProp.Split(',',StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.ToList();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
