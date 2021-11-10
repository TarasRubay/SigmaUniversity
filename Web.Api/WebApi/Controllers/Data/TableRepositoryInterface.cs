using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.Data
{
    public class TableRepositoryInterface<T> : IRepository<T> where T : class
    {
        private readonly TableContext _context;
        protected DbSet<T> DbSet;

        public TableRepositoryInterface(TableContext context)
        {
            _context = context;
            context.Database.EnsureCreated();
            DbSet = _context.Set<T>();
        }
        public T Create(T entity)
        {
            var result = DbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);    
        }

        public void Remove(int id)
        {
            DbSet.Remove(DbSet.Find(id));
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
