using DoctorAppointment.Services.Receipts.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Receipts
{
  public static class GetReceiptDtoFactory
    {
        public static GetReceiptDto Create()
        {
            return new GetReceiptDto
            {
                Id = 1,
                DoctorId = 1,
                PatientId = 1,
                ReserveDate = new DateTime(2024, 03, 04)
            };
        }
    }
}
