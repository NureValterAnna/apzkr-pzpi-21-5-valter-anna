using Domain.Resources;

namespace Domain.Exceptions;

public class MedicineStockAlreadyExistsException : Exception
{
    public MedicineStockAlreadyExistsException() : base(Resource.MedicineStockAlreadyExistsException) { }
}
