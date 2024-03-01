using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Entities.Receipts;

namespace DoctorAppointment.Entities.Doctors;

public class Doctor
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Field { get; set; }
    public string NationalCode { get; set; }
    public List<Receipt> Receipts { get; set; }
}
