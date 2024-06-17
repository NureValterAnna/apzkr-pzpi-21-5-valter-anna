using Application.Prescriptions.Queries;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPrescriptionService
{
    Task<List<Prescription>> GetPrescriptionsByEmailAsync();
}