using Domain.Resources;

namespace Domain.Exceptions;

public class NotEnoughMedicineException : Exception
{
    public NotEnoughMedicineException() : base(Resource.NotEnoughMedicineException) { }
}
