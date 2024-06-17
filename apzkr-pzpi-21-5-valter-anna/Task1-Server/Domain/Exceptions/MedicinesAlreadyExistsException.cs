using Domain.Resources;

namespace Domain.Exceptions;

public class MedicinesAlreadyExistsException : Exception
{
    public MedicinesAlreadyExistsException() : base(Resource.MedicinesAlreadyExistsException) { }
}
