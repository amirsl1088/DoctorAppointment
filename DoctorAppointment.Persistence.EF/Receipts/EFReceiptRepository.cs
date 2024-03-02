using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Entities.Receipts;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Services.Receipts.Contracts.Dto;
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

        public int CountOfReceipts()
        {
            return _context.Receipts.Count();
        }

        public void Delete(Receipt receipt)
        {
            _context.Receipts.Remove(receipt);
        }

        public async Task<Doctor?> FindDoctorById(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<Receipt>?> FindDoctorReceipt(int id)
        {
           return await _context.Receipts.Where(_ => _.DoctorId == id).ToListAsync();

            
            
        }


        public async Task<Patient?> FindPatientById(int id)
        {
          return  await _context.Patients.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<Receipt?> FindReceiptById(int id)
        {
            return await _context.Receipts.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<GetReceiptDto>> GetAll()
        {
            return  _context.Receipts.Select(_ => new GetReceiptDto
            {
                Id = _.Id,
                DoctorId = _.DoctorId,
                PatientId = _.PatientId,
                ReserveDate = _.ReserveDate
            }).ToList();
        }

       
    }
}
