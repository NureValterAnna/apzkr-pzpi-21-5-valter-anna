using Domain.Resources;

namespace Domain.Exceptions;

public class PrescriptionHasExpiredException : Exception
{
    public PrescriptionHasExpiredException() : base(Resource.PrescriptionHasExpiredException) { }
}
