namespace Service.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICustomerRepository Customer { get; }
    Task<int> SaveChangesAsync();
}