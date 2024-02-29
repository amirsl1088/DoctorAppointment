using DoctorAppointment.Contracts.Interfaces;
using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Contracts.Dto;
using DoctorAppointment.Services.Doctors.Exceptions;

namespace DoctorAppointment.Services.Doctors;

public class DoctorAppService : DoctorService
{
    private readonly DoctorRepository _repository;
    private readonly UnitOfWork _unitOfWork;

    public DoctorAppService(
        DoctorRepository repository,
        UnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(AddDoctorDto dto)
    {
        var doctor = new Doctor()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Field = dto.Field,
            NationalCode=dto.NationalCode
        };
        var result = _repository.IsExistNationalCode(dto.NationalCode);
        if (result is true)
        {
            throw new DoctorsCannotHaveSameNationalCode();
        }

        _repository.Add(doctor);
        await _unitOfWork.Complete();
    }

    public async Task Delete(int id)
    {
        var doctor = await _repository.FindById(id);
        if(doctor is null)
        {
            throw new DoctorIdNotFound();
        }
        _repository.Delete(doctor);
        await _unitOfWork.Complete();
    }

    public List<GetDoctorDto> GetDoctors()
    {
        var doctors = _repository.GetDoctors();
        
        return doctors;
    }

    public async Task Update(int id, UpdateDoctorDto dto)
    {
        var doctor = await _repository.FindById(id);
        if(doctor is null)
        {
            throw new DoctorIdNotFound();
        }

        doctor.FirstName = dto.FirstName;
        doctor.LastName = dto.LastName;
        doctor.Field = dto.Field;
        doctor.NationalCode = dto.NationalCode;
       

        await _unitOfWork.Complete();
    }

    
}