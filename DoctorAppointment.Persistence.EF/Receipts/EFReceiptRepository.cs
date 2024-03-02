using DoctorAppointment.Entities.Receipts;
using DoctorAppointment.Persistence.EF;
using Microsoft.EntityFrameworkCore;

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

        public async Task FindDoctorById(int id)
        {
            await _context.Doctors.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<Receipt>> FindDoctorReceipt(int id)
        {
            var doctor =await _context.Doctors.FirstOrDefaultAsync(_ => _.Id == id);
           
            return  doctor.Receipts;
        }

        public async Task FindPatientById(int id)
        {
            await _context.Patients.FirstOrDefaultAsync(_ => _.Id == id);
        }
    }
}
