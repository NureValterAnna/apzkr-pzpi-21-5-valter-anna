using Domain.Resources;

namespace Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base(Resource.UserNotFoundException)
    {
    }
}
