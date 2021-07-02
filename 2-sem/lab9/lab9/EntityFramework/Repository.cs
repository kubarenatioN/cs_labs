using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace lab9.EntityFramework
{
    public class Repository<T> where T : class
    {
        internal static Lab9DbContext context;
        internal DbSet<T> dbSet;
        public Repository(Lab9DbContext cntxt)
        {
            context = cntxt;
            dbSet = context.Set<T>();
        }
        public void Create(T entity)
        {
            //Console.WriteLine(dbSet.Local);
            dbSet.Add(entity);
            context.SaveChanges();
        }
        public List<T> GetAll()
        {
            return dbSet.ToList();
        }
        public List<Team> GetAllTeamsWithPlayers()
        {
            return context.Set<Team>().Include(team => team.Players).ToList();
        }
        public List<Team> GetTeamsWithPlayers(Func<Team, bool> predicate)
        {
            return context.Set<Team>().Include(team => team.Players).Where(predicate).ToList();
        }
        public List<Player> GetPlayersWithTeams(Func<Player, bool> predicate)
        {
            return context.Set<Player>().Include(p => p.Team).Where(predicate).ToList();
        }
        public List<T> Get(Func<T, bool> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }
        public void Update(T entity)
        {
            //Console.WriteLine(dbSet.Local);
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
