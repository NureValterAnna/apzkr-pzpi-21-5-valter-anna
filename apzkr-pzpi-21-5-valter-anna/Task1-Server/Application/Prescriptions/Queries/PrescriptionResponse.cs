using Application.Users.Queries.GetUser;
using AutoMapper;
using Domain.Entities;

namespace Application.Prescriptions.Queries;

public class PrescriptionResponse
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public UserInfo User { get; set; } = null!;

    public int MedicineId { get; set; }
    public MedicineInfo Medicine { get; set; } = null!;

    public DateTime PrescriptionDateStart { get; set; }

    public DateTime PrescriptionDateEnd { get; set; }

    public double Dose { get; set; }

    public int TimesPerDay { get; set; }

    public List<TransactionInfo> Transactions { get; set; } = new List<TransactionInfo>();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Prescription, PrescriptionResponse>();
            CreateMap<User, UserInfo>();
            CreateMap<Medicine, MedicineInfo>();
            CreateMap<Transaction, TransactionInfo>();
        }
    }
}
