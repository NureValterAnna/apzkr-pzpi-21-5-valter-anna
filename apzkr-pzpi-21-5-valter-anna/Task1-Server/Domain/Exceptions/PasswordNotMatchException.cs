using Domain.Resources;

namespace Domain.Exceptions;

public class PasswordNotMatchException : Exception
{
    public PasswordNotMatchException() : base(Resource.PasswordNotMatchException) 
    {
    }
}
