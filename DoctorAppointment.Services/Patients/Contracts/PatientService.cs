using DoctorAppointment.Services.Patients.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Patients.Contracts
{
    public interface PatientService
    {
        Task Add(AddPatientDto dto);
        Task Update(int id, UpdatePatientDto dto);
        List<GetPatientDto> GetPatients();
        Task Delete(int id);
    }
}
