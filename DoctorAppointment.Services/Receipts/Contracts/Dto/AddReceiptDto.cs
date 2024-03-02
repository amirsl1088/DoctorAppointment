using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorAppointment.Services.Unit.Tests.Receipts
{
    public class AddReceiptDto
    {
        
        
       
        public DateTime ReserveDate { get; set; }
        [ForeignKey("DoctorId")]
        public int DoctorId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientId { get; set; }
    }
}
