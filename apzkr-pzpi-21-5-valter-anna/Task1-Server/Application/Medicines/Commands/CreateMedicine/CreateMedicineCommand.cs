using MediatR;

namespace Application.Medicines.Commands.CreateMedicine;

public record CreateMedicineCommand : IRequest<int>
{
    public string Title { get; set; }

    public string Description { get; set; }
}
