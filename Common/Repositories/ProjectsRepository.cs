using Common.Entities;

namespace Common.Repositories
{
    public class ProjectsRepository : BaseRepository<Project>
    {
        public ProjectsRepository()
        {
        }

        public ProjectsRepository(UnitOfWork uow) : base(uow)
        {
        }
    }
}
