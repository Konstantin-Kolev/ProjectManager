using Common.Entities;

namespace Common.Repositories
{
    public class HoursLogsRepository : BaseRepository<HoursLog>
    {
        public HoursLogsRepository()
        {
        }

        public HoursLogsRepository(UnitOfWork uow) : base(uow)
        {
        }
    }
}
