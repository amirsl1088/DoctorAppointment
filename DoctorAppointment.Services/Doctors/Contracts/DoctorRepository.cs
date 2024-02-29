using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Services.Doctors.Contracts.Dto;

namespace DoctorAppointment.Services.Doctors.Contracts;

public interface DoctorRepository
{
    void Add(Doctor doctor);
    Task<Doctor?> FindById(int id);
    bool IsExistNationalCode( string nationalcode);
    List<GetDoctorDto> GetDoctors();
    void Delete(Doctor doctor);
}