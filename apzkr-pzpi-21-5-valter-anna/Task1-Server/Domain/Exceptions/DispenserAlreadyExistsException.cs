using Domain.Resources;
namespace Domain.Exceptions;

public class DispenserAlreadyExistsException : Exception
{
    public DispenserAlreadyExistsException() : base(Resource.DispenserAlreadyExistsException)
    {

    }
}
