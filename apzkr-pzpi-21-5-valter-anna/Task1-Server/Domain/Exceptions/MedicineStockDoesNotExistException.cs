using Domain.Resources;

namespace Domain.Exceptions;

public class MedicineStockDoesNotExistException : Exception
{
    public MedicineStockDoesNotExistException() : base(Resource.MedicineStockDoesNotExistException) { }
}
