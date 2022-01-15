using Data;
using Service.Interfaces;

namespace Service.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    public ICustomerRepository Customer { get; private set; }

    public UnitOfWork(DataContext context)
    {
        _context = context;
        Customer = new CustomerRepository(context);
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
