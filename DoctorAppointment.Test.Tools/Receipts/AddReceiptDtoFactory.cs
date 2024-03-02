using DoctorAppointment.Services.Unit.Tests.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Receipts
{
    public static class AddReceiptDtoFactory
    {
        public static AddReceiptDto Create(DateTime? reservedate = null, int? patientid = null)
        {
            return  new AddReceiptDto
            {
                DoctorId = 1,
                PatientId = patientid??1,
                ReserveDate = reservedate??new DateTime(2024,03,04)
            };
        }
    }
}
