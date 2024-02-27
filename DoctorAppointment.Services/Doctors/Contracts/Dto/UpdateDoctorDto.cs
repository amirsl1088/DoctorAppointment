using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Services.Doctors.Contracts.Dto;

public class UpdateDoctorDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Field { get; set; }
    [Required]
    public string NationalCode { get; set; }
}