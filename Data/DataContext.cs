using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Account> Accounts => Set<Account>();
    public virtual DbSet<Order> Orders => Set<Order>();
}
