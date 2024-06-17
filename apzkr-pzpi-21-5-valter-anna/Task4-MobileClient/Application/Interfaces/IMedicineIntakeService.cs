using Application.MedicineIntake.Commands;

namespace Application.Interfaces;

public interface IMedicineIntakeService
{
    public Task MedicineIntake(MedicineIntakeCommand request);
}