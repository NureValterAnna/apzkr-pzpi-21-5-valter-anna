using Domain.Resources;

namespace Domain.Exceptions;

public class DailyDoseAlreadyTakenException : Exception
{
    public DailyDoseAlreadyTakenException(): base(Resource.DailyDoseAlreadyTakenException) { }
}
