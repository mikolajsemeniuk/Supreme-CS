namespace Service.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAccountRepository Account { get; }
    Task<int> SaveChangesAsync();
}