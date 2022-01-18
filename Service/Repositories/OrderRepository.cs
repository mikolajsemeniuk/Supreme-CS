using Data;
using Data.Entities;
using Service.Interfaces;

namespace Service.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(DataContext context) : base(context)
    {
    }
}