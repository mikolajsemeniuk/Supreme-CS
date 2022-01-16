namespace Data.Entities;

public class BaseEntity
{
    public Guid AccountId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}