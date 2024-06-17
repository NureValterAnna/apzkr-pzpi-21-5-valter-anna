using Domain.Resources;

namespace Domain.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException() : base(Resource.UserAlreadyExistsException)
    {
    }
}
