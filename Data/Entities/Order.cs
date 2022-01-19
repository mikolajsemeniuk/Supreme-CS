using System.Text.Json.Serialization;
using Data.Enums;

namespace Data.Entities;

public class Order : BaseEntity
{
    public string Name { get; set; }
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Awaiting;
    public long FailureCounter { get; set; } = 0;
    public Guid AccountId { get; set; }
    
    [JsonIgnore]
    public Account Account { get; set; } = null!;

    public Order(string name, Guid accountId)
    {
        Name = name;
        AccountId = accountId;
    }
}