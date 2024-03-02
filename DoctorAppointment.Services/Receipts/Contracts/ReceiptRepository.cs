using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Entities.Receipts;
using DoctorAppointment.Services.Receipts.Contracts.Dto;

namespace DoctorAppointment.Services.Unit.Tests.Receipts
{
    public interface ReceiptRepository
    {
        void Add(Receipt receipt);
       Task<Doctor> FindDoctorById(int id);
        Task<Patient> FindPatientById(int id);
       Task<List<Receipt>?> FindDoctorReceipt(int id);
        int CountOfReceipts();
       Task<List<GetReceiptDto>> GetAll();
        Task<Receipt?> FindReceiptById(int id);
        void Delete(Receipt receipt);
    }
}
