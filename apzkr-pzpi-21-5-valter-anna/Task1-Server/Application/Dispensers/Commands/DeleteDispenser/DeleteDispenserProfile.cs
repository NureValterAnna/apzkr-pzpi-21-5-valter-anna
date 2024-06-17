using AutoMapper;
using Domain.Entities;

namespace Application.Dispensers.Commands.DeleteDispenser;

public class DeleteDispenserProfile : Profile
{
    public DeleteDispenserProfile()
    {
        CreateMap<DeleteDispenserCommand, Dispenser>();
    }
}
