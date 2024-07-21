using Microsoft.EntityFrameworkCore;
using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Infrastructure.Repositories;

internal abstract class Repository<TEntity>
    where TEntity : Entity
{
    protected readonly ApplicationDbContext _context;

    protected Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _context
            .Set<TEntity>()
            .FirstOrDefaultAsync(entity => entity.Id == id);
    }

}
