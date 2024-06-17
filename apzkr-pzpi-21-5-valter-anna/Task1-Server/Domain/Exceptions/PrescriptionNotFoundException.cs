using Domain.Resources;

namespace Domain.Exceptions;

public class PrescriptionNotFoundException : Exception
{
    public PrescriptionNotFoundException() : base(Resource.PrescriptionNotFoundException) { }
}
