using AutoMapper;
using Domain.Entities;

namespace Application.Users.Queries.GetUser;

public class UserResponse
{
    public int Id { get; set;}

    public string Name { get; set; }

    public string Surname { get; set; }

    public int? Age { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserResponse>();
        }
    }
}
