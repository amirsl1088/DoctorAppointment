namespace DoctorAppointment.Services.Unit.Tests.Receipts
{
    public class AddReceiptDto
    {
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime ReserveDate { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
