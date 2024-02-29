using DoctorAppointment.Services.Doctors.Contracts.Dto;

namespace DoctorAppointment.Services.Doctors.Contracts;

public interface DoctorService
{
    Task Add(AddDoctorDto dto);
   Task<List<GetDoctorDto>> GetDoctors();
    Task Update(int id,UpdateDoctorDto dto);
    Task Delete(int id);
}