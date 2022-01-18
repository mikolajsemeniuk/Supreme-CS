namespace Service.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository Account { get; }
    IOrderRepository Order { get; }
    Task<int> SaveChangesAsync();
}