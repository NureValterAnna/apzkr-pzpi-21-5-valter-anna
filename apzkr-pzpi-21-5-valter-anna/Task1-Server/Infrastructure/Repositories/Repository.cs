using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async virtual Task Create(T entity)
    {
        await _context.AddAsync(entity);
    }

    public async virtual Task Delete(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual Task<T> GetAsync(int id, CancellationToken cancellationToken)
    {
       return _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public virtual Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _dbSet.ToListAsync();
    }

    public async virtual Task Update(T entity, CancellationToken cancellationToken)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
