using DoctorAppointment.Services.Receipts.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Receipts
{
    public static class UpdateReceiptDtoFactory
    {
        public static UpdateReceiptDto Create(DateTime? reservedate = null)
        {
            return new UpdateReceiptDto
            {
                ReserveDate = reservedate ?? new DateTime(2024, 03, 04)
            };
        }
    }
}
