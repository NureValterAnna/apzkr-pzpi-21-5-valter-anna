using AutoMapper;
using Domain.Entities;

namespace Application.Users.Commands.Delete;

public class DeleteUserProfile : Profile
{
    public DeleteUserProfile() 
    {
        CreateMap<DeleteUserCommand, User>();
    }
}
