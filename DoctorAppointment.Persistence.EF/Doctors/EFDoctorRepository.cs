using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Persistence.EF.Doctors;

public class EFDoctorRepository : DoctorRepository
{
    private readonly EFDataContext _context;

    public EFDoctorRepository(EFDataContext context)
    {
        _context = context;
    }

    public void Add(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
    }

    public void Delete(Doctor doctor)
    {
        _context.Doctors.Remove(doctor);
    }

    public async Task<Doctor?> FindById(int id)
    {
        return await _context.Doctors.FirstOrDefaultAsync(_ => _.Id == id);
    }

    public List<GetDoctorDto> GetDoctors()
    {
        return _context.Doctors.Select(_ => new GetDoctorDto
        {
            Id = _.Id,
            FirstName = _.FirstName,
            LastName = _.LastName,
            Field = _.Field,
            NationalCode = _.NationalCode
        }).ToList();
    }

    public bool IsExistNationalCode(string nationalcode)
    {
        return _context.Doctors.Any(_ => _.NationalCode == nationalcode);
    }
}