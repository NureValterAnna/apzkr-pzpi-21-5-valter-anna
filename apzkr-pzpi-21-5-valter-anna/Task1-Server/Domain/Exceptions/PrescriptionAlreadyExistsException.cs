using Domain.Resources;

namespace Domain.Exceptions;

public class PrescriptionAlreadyExistsException : Exception
{
    public PrescriptionAlreadyExistsException() : base(Resource.PrescriptionAlreadyExistsException)
    {

    }
}
