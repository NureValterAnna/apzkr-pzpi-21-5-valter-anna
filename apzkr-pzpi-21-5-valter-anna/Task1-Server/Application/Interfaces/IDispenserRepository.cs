using Application.Dispensers.Commands.CreateDispenser;
using Application.Dispensers.Commands.DeleteDispenser;
using Application.Dispensers.Commands.UpdateDispenser;
using Application.Dispensers.Commands.UpdateDispenserTempereture;
using Application.Dispensers.Queries.GetDispensers;
using Domain.Entities;

namespace Application.Interfaces;

public interface IDispenserRepository : IRepository<Dispenser>
{
    Task<int> CreateDispenser(CreateDispenserCommand request, CancellationToken cancellationToken);

    Task<int> DeleteDispenser(DeleteDispenserCommand request, CancellationToken cancellationToken);

    Task UpdateLocation(UpdateDispenserLocationCommand request, CancellationToken cancellationToken);

    Task UpdateTemperature(UpdateDispenserTemperetureCommand request, CancellationToken cancellationToken);

    Task<List<Dispenser>> GetAllAsync(GetDispensersQuery request, CancellationToken cancellationToken);

}
