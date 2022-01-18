using System.Linq.Expressions;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Service.Interfaces;
using Service.Enums;
using Data.Entities;

namespace Service.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DataContext _context;
    internal DbSet<T> _set;

    public BaseRepository(DataContext context)
    {
        _context = context;
        _set = context.Set<T>();
    }

    public async Task<IEnumerable<T>> AllAsync(
        Expression<Func<T, bool>>? filter = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>>? order = null, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int skip = 0, int take = int.MaxValue, Track track = Track.NoTracking)
    {
        IQueryable<T> query = _set;
        switch (track)
        {
            case Track.NoTracking:
                query = query.AsNoTracking();
                break;
            case Track.NoTrackingWithIdentityResolution:
                query = query.AsNoTrackingWithIdentityResolution();
                break;
            default:
                query = query.AsTracking();
                break;
        }
        query = skip == 0 ? query.Take(take) : query.Skip(skip).Take(take);
        query = filter is null ? query : query.Where(filter);
        query = order is null ? query : order(query);
        query = include is null ? query : include(query);
        return await query.ToListAsync();
    }

    public async Task<T?> SingleAsync(
        Expression<Func<T, bool>> filter,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Track track = Track.Tracking)
    {

        IQueryable<T> query = _set;
        switch (track)
        {
            case Track.NoTracking:
                query = query.AsNoTracking();
                break;
            case Track.NoTrackingWithIdentityResolution:
                query = query.AsNoTrackingWithIdentityResolution();
                break;
            default:
                query = query.AsTracking();
                break;
        }
        query = filter is null ? query : query.Where(filter);
        query = include is null ? query : include(query);
        return await query.SingleOrDefaultAsync();
    }
    
    public void Add(T entity)
    {
        _context.Add(entity);
    }

    public void Update(T entity)
    {
        entity.UpdatedAt = DateTime.Now;
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(T entity)
    {
        _context.Remove(entity);
    }
}