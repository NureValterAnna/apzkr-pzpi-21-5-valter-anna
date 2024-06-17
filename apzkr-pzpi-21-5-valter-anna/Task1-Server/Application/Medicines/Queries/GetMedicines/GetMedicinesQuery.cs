using MediatR;

namespace Application.Medicines.Queries.GetMedicines;

public record GetMedicinesQuery : IRequest<List<MedicineResponse>>
{
}
