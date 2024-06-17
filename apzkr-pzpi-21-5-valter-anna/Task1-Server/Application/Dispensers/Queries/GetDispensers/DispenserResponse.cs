using Application.Users.Queries.GetUser;
using AutoMapper;
using Domain.Entities;

namespace Application.Dispensers.Queries.GetDispensers;

public class DispenserResponse
{
    public int Id { get; set; }

    public string DispensorName { get; set; }

    public string Location { get; set; }

    public double StorageTemperature { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Dispenser, DispenserResponse>();
        }
    }
}
