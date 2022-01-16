using System.Linq.Expressions;
using Data.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Service.Enums;

namespace Service.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> AllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? order = null, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        // Expression<Func<T, object>>? select = null,
        int skip = 0,
        int take = int.MaxValue,
        Track track = Track.NoTracking);

    Task<T?> SingleAsync(
        Expression<Func<T, bool>> filter, Func<IQueryable<T>, 
        IIncludableQueryable<T, object>>? include = null,
        // Expression<Func<T, object>>? select = null,
        Track track = Track.Tracking);

    void Add(T entity);

    void Update(T entity);

    void Remove(T entity);
}