using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace lab10
{
    public class Repository<T> where T : class
    {
        internal static FacultyDbContext context;
        internal DbSet<T> dbSet;
        public Repository(FacultyDbContext cntxt)
        {
            context = cntxt;
            dbSet = context.Set<T>();
        }
        public void Create(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }
        public List<T> GetAll()
        {
            return dbSet.ToList();
        }
        public List<T> Get(Func<T, bool> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }
        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Remove(T entity)
        {
            if (!dbSet.Local.Contains(entity))
            {
                dbSet.Attach(entity);
            }
            context.Entry(entity).State = EntityState.Deleted;
            dbSet.Remove(entity);
            context.SaveChanges();
        }
    }
}
