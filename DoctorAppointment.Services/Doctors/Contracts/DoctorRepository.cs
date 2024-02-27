using DoctorAppointment.Entities.Doctors;

namespace DoctorAppointment.Services.Doctors.Contracts;

public interface DoctorRepository
{
    void Add(Doctor doctor);
    Task<Doctor?> FindById(int id);
    bool IsExistNationalCode( string nationalcode);
    List<Doctor> GetDoctors();
    void Delete(Doctor doctor);
}