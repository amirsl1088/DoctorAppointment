using DoctorAppointment.Entities.Receipts;

namespace DoctorAppointment.Services.Unit.Tests.Receipts
{
    public interface ReceiptRepository
    {
        void Add(Receipt receipt);
       Task FindDoctorById(int id);
        Task FindPatientById(int id);
       Task<List<Receipt>> FindDoctorReceipt(int id);
       
    }
}
