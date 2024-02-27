using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Services.Patients.Contracts.Dto
{
    public class UpdatePatientDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string NationalCode { get; set; }
    }
}
