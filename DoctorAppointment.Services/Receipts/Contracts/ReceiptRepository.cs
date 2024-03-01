using DoctorAppointment.Entities.Receipts;

namespace DoctorAppointment.Services.Unit.Tests.Receipts
{
    public interface ReceiptRepository
    {
        void Add(Receipt receipt);
        bool IsExistDoctor(string doctorName);
        bool IsExistPatient(string patientName);
       
    }
}
