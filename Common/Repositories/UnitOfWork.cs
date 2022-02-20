using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Common.Repositories
{
    public class UnitOfWork
    {
        public DbContext Context { get; set; }

        private IDbContextTransaction Transaction { get; set; }

        public UnitOfWork()
        {
            Context = new ProjectManagerAPIDbContext();
        }

        public void BeginTransation()
        {
            Transaction = Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (Transaction != null)
                Transaction.Commit();
        }

        public void Rollback()
        {
            if (Transaction != null)
                Transaction.Rollback();
        }
    }
}
