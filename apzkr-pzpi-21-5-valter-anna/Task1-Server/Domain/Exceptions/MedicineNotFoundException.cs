using Domain.Entities;
using Domain.Resources;

namespace Domain.Exceptions;

public class MedicineNotFoundException : Exception
{
    public MedicineNotFoundException() : base(Resource.MedicineNotFoundException)
    {

    }
}
