using Data;
using Data.Entities;
using Service.Interfaces;

namespace Service.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    private readonly DataContext _context;
    public AccountRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
