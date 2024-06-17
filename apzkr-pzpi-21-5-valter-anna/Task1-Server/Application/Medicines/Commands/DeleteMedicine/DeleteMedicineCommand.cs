using MediatR;

namespace Application.Medicines.Commands.DeleteMedicine;

public record DeleteMedicineCommand : IRequest<int>
{
    public int Id { get; set; } 
}
