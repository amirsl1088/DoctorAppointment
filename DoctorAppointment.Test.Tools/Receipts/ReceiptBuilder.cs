using DoctorAppointment.Entities.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Receipts
{
    public class ReceiptBuilder
    {
        private readonly Receipt _receipt;
        public ReceiptBuilder()
        {
            _receipt = new Receipt
            {
                DoctorId = 1,
                PatientId = 1,
                ReserveDate = new DateTime(2024, 03, 04)
            };
        }
        public ReceiptBuilder WithDoctorId(int doctorid)
        {
            _receipt.DoctorId = doctorid;
            return this;
        }
        public ReceiptBuilder WithPatientId(int patientid)
        {
            _receipt.PatientId = patientid;
            return this;
        }
        public ReceiptBuilder WithDateTime(DateTime reservedate)
        {
            _receipt.ReserveDate = reservedate;
            return this;
        }
        public Receipt Build()
        {
            return _receipt;
        }
    }
}
