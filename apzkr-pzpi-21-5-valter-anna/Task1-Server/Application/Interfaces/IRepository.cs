using Domain.Entities;

namespace Application.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task Create(T entity);

    Task Delete(T entity);

    Task Update(T entity, CancellationToken cancellationToken);

    Task<T> GetAsync(int id, CancellationToken cancellationToken);

    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
}
