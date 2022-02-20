using Common.Entities;

namespace Common.Repositories
{
    public class AssignmentsRepository : BaseRepository<Assignment>
    {
        public AssignmentsRepository()
        {
        }

        public AssignmentsRepository(UnitOfWork uow) : base(uow)
        {
        }
    }
}
