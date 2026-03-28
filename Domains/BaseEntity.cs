namespace Domains
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();          

        public Guid? UpdatedBy { get; set; }

        public int CurrentState { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;  // avoids SqlDateTime overflow

        public Guid CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
