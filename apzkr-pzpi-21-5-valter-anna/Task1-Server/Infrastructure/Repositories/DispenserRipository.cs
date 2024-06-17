using Application.Dispensers.Commands.CreateDispenser;
using Application.Dispensers.Commands.DeleteDispenser;
using Application.Dispensers.Commands.UpdateDispenser;
using Application.Dispensers.Commands.UpdateDispenserTempereture;
using Application.Dispensers.Queries.GetDispensers;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DispenserRipository : Repository<Dispenser>, IDispenserRepository
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IHubContext<NotificationHub, INotificationHub> _notificationHub;

    private const double THRESHOLD_TEMPERATURE = 26;

    public DispenserRipository(AppDbContext context, IMapper mapper, IUnitOfWork unitOfWork, IHubContext<NotificationHub, INotificationHub> notificationHub) : base(context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _notificationHub = notificationHub;
    }

    public async Task<int> CreateDispenser(CreateDispenserCommand request, CancellationToken cancellationToken)
    {
        var dispenser = await _context.Dispensers.FirstOrDefaultAsync(x => x.DispensorName == request.DispensorName && x.Location == request.Location);
        if (dispenser is not null)
        {
            throw new DispenserAlreadyExistsException();
        }
        var newDispenser = _mapper.Map<Dispenser>(request);
        await base.Create(newDispenser);
        await _unitOfWork.SaveChanges(cancellationToken);
        return newDispenser.Id;
    }

    public async Task<int> DeleteDispenser(DeleteDispenserCommand request, CancellationToken cancellationToken)
    {
        var removedDispenser = await _context.Dispensers.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (removedDispenser is null)
        {
            throw new DispenserNotFoundException();
        }
        await base.Delete(removedDispenser);
        return removedDispenser.Id;
    }

    public async Task<List<Dispenser>> GetAllAsync(GetDispensersQuery request, CancellationToken cancellationToken)
    {
        var dispensers = await base.GetAllAsync(cancellationToken);

        if (request.TemperatureUnit == "F")
        {
            foreach (var dispenser in dispensers)
            {
                dispenser.StorageTemperature = ConvertToFahrenheit(dispenser.StorageTemperature);
            }
        }

        return dispensers;
    }

    private double ConvertToFahrenheit(double celsiusTemperature)
    {
        return celsiusTemperature * 9 / 5 + 32;
    }

    public async Task UpdateLocation(UpdateDispenserLocationCommand request, CancellationToken cancellationToken)
    {
        var dispenser = await _context.Dispensers.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (dispenser is null)
        {
            throw new DispenserNotFoundException();
        }
        else
        {
            dispenser.Location = request.Location;
            await base.Update(dispenser, cancellationToken);
        }
    }

    public async Task UpdateTemperature(UpdateDispenserTemperetureCommand request, CancellationToken cancellationToken)
    {
        var dispenser = await _context.Dispensers.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (dispenser is null)
        {
            throw new DispenserNotFoundException();
        }
        else
        {
            dispenser.StorageTemperature = request.StorageTemperature;
            await base.Update(dispenser, cancellationToken);

            if (request.StorageTemperature > THRESHOLD_TEMPERATURE)
            {
                await _notificationHub.Clients.All.SendNotification("Temperature exceeded!");
            }
        }
    }
}
