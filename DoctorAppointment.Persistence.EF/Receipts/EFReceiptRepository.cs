using DoctorAppointment.Entities.Receipts;
using DoctorAppointment.Persistence.EF;

namespace DoctorAppointment.Services.Unit.Tests.Receipts
{
    public class EFReceiptRepository:ReceiptRepository
    {
        private EFDataContext _context;

        public EFReceiptRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Receipt receipt)
        {
            _context.Receipts.Add(receipt);
        }

        

        public bool IsExistDoctor(string doctorName)
        {
            return _context.Doctors.Any(_ => _.FirstName == doctorName);
        }

        public bool IsExistPatient(string patientName)
        {
            return _context.Patients.Any(_ => _.FirstName == patientName);
        }
    }
}
