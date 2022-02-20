using Common.Entities;

namespace Common.Repositories
{
    public class RefreshTokensRepository : BaseRepository<RefreshToken>
    {
        public RefreshTokensRepository()
        {
        }

        public RefreshTokensRepository(UnitOfWork uow) : base(uow)
        {
        }

        public void InvalidateTokens(int userId)
        {
            IEnumerable<RefreshToken> invalidTokens = Items;
            invalidTokens = invalidTokens.Where(r => r.UserId == userId);
            invalidTokens = invalidTokens.Select(r=>
            {
                r.IsUsed = true;
                return r;
            });

            Items.UpdateRange(invalidTokens);

            Context.SaveChanges();

        }
    }
}
