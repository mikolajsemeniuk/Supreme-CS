using Data;
using Service.Interfaces;

namespace Service.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    public IAccountRepository Account { get; private set; }

    public IOrderRepository Order { get; private set; }

    public UnitOfWork(DataContext context)
    {
        _context = context;
        Account = new AccountRepository(context);
        Order = new OrderRepository(context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
