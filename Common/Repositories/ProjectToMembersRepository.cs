using Common.Entities;

namespace Common.Repositories
{
    public class ProjectToMembersRepository : BaseRepository<ProjectToMember>
    {
        public ProjectToMembersRepository()
        {
        }

        public ProjectToMembersRepository(UnitOfWork uow) : base(uow)
        {
        }
    }
}
