using Common.Entities;

namespace Common.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        public UsersRepository()
        {
        }

        public UsersRepository(UnitOfWork uow) : base(uow)
        {
        }
    }
}
