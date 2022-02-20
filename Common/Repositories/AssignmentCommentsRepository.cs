using Common.Entities;

namespace Common.Repositories
{
    public class AssignmentCommentsRepository : BaseRepository<AssignmentComment>
    {
        public AssignmentCommentsRepository()
        {
        }

        public AssignmentCommentsRepository(UnitOfWork uow) : base(uow)
        {
        }
    }
}
