using Domain.Resources;

namespace Domain.Exceptions;

public class DispenserNotFoundException : Exception
{
    public DispenserNotFoundException() : base(Resource.DispenserNotFoundException) { }
}
