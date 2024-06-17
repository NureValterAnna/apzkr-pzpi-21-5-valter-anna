using AutoMapper;
using Domain.Entities;

namespace Application.Users.Commands.Register;

public class RegisterProfile : Profile
{
    public RegisterProfile()
    {
        CreateMap<RegisterCommand, User>();
    }
}
