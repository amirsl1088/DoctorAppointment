using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Patients.Contracts
{
    public interface PatientRepository
    {
        void Add(Patient patient);
        bool IsExistNationalCode(string nationalcode);
        Task<Patient?> FindById(int id);
        void Update();
        List<Patient> GetPatients();
        void Delete(Patient patient);


    }
}
