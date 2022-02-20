using Common.Entities;
using Common.Tools;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Common.Repositories
{
    public class BaseRepository<T>
        where T : BaseEntity
    {
        protected DbContext Context { get; set; }

        protected DbSet<T> Items { get; set; }

        public BaseRepository(UnitOfWork uow)
        {
            Context = uow.Context;
            Items = Context.Set<T>();
        }

        public BaseRepository()
        {
            Context = new ProjectManagerAPIDbContext();
            Items = Context.Set<T>();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null,
                                int page = GlobalConstants.DefaultPage,
                                int pageSize = int.MaxValue)
        {
            IQueryable<T> query = Items;

            if (filter != null)
                query = query.Where(filter);

            return query.Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = Items;

            if (filter != null)
                query = query.Where(filter);

            return query.FirstOrDefault();
        }

        public int Count(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = Items;

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }

        public void Save(T item)
        {
            if (item.Id <= 0)
                Items.Add(item);
            else
                Items.Update(item);

            Context.SaveChanges();
        }

        public void Delete(T item)
        {
            Items.Remove(item);

            Context.SaveChanges();
        }
    }
}
