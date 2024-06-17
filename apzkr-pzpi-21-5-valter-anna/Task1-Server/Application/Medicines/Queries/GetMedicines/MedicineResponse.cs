using Application.Users.Queries.GetUser;
using AutoMapper;
using Domain.Entities;

namespace Application.Medicines.Queries.GetMedicines;

public class MedicineResponse
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Medicine, MedicineResponse>();
        }
    }

}
