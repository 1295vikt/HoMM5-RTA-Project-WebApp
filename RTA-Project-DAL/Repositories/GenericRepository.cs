using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        TEntity FindById(int id);

        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> QueryAll();

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includes);

        TEntity GetFirstOrDefault(
        Expression<Func<TEntity, bool>> filter = null,
        params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        void Create(TEntity item);
        void RemoveById(int id);
        void Remove(TEntity item);
        void Update(TEntity item);
        void Dispose();
    }

    public class GenericRepository<TEntity> : IDisposable, IGenericRepository<TEntity> where TEntity : class
    {
        readonly DbContext _context;
        readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> QueryAll()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query.ToList();
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
        params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return query.FirstOrDefault(filter);
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        public virtual TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public virtual void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public virtual void RemoveById(int id)
        {
            _dbSet.Remove(_dbSet.Find(id));
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}