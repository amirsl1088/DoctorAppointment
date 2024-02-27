using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Services.Patients.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Services.Unit.Tests
{
    public class EFPatientRepository:PatientRepository
    {
        private EFDataContext _context;

        public EFPatientRepository(EFDataContext context)
        {
            this._context = context;
        }

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
        }

        public void Delete(Patient patient)
        {
            _context.Patients.Remove(patient);
        }

        public async Task<Patient?> FindById(int id)
        {
          return  await _context.Patients.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public List<Patient> GetPatients()
        {
            return _context.Patients.ToList();
        }

        public bool IsExistNationalCode(string nationalcode)
        {
            return _context.Patients.Any(_ => _.NationalCode == nationalcode);
        }

        public void Update()
        {
            
        }
    }
}
