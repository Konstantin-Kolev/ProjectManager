namespace Common.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; } //is Giud

        public bool IsUsed { get; set; }

        public int UserId { get; set; }
    }
}
