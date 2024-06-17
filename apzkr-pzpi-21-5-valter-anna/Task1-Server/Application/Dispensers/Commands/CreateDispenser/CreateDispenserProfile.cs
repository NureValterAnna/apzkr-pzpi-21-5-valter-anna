using AutoMapper;
using Domain.Entities;

namespace Application.Dispensers.Commands.CreateDispenser;

public class CreateDispenserProfile : Profile
{
    public CreateDispenserProfile()
    {
        CreateMap<CreateDispenserCommand, Dispenser>();
    }
}
