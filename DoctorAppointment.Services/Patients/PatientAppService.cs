using DoctorAppointment.Contracts.Interfaces;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Services.Doctors.Exceptions;
using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Services.Patients.Contracts.Dto;
using DoctorAppointment.Services.Patients.Exeptions;

namespace DoctorAppointment.Services.Patients
{
    public class PatientAppService : PatientService
    {
        private PatientRepository _repository;
        private UnitOfWork _unitOfWork;

        public PatientAppService(PatientRepository eFPatientRepository, UnitOfWork eFUnitOfWork)
        {
            _repository = eFPatientRepository;
            _unitOfWork = eFUnitOfWork;
        }

        public async Task Add(AddPatientDto dto)
        {
            var patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                NationalCode = dto.NatinalCode

            };
            var result = _repository.IsExistNationalCode(dto.NatinalCode);
            if (result is true)
            {
                throw new PatientsCannotHaveSameNationalCode();
            }
            _repository.Add(patient);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var patient = await _repository.FindById(id);
            if (patient is null)
            {
                throw new PatientNotFound();
            }
            _repository.Delete(patient);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetPatientDto>> GetPatients()
        {
            return await _repository.GetPatients();
           
        }

        public async Task Update(int id, UpdatePatientDto updatedto)
        {
            var patient = await _repository.FindById(id);
            if(patient is null)
            {
                throw new PatientNotFound();
            }
            patient.FirstName = updatedto.FirstName;
            patient.LastName = updatedto.LastName;
            patient.NationalCode = updatedto.NationalCode;
            await _unitOfWork.Complete();
        }
    }
}
