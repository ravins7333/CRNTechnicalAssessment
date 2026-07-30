namespace CRN.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime CreatedDate { get; set; }

        // Relation with User
        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}