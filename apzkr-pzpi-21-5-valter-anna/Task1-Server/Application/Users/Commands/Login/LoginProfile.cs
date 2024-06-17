using Application.Users.Commands.Register;
using AutoMapper;
using Domain.Entities;

namespace Application.Users.Commands.Login;

public class LoginProfile : Profile
{
    public LoginProfile()
    {
        CreateMap<LoginCommand, User>();
    }
}
