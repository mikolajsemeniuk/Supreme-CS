using System.Text.Json.Serialization;
using Data.Enums;

namespace Data.Entities;

public class Order
{
    public Guid OrderId { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Awaiting;
    public long FailureCounter { get; set; } = 0;
    public Guid AccountId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    
    [JsonIgnore]
    public Account Account { get; set; } = null!;

    public Order(string name, Guid accountId)
    {
        Name = name;
        AccountId = accountId;
    }
}