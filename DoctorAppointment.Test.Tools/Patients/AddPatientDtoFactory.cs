using DoctorAppointment.Services.Patients.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Patients
{
    public static class AddPatientDtoFactory
    {
        public static AddPatientDto Create(string? nationalcode=null)
        {
            var dto = new AddPatientDto
            {
                FirstName = "htdtjg",
                LastName = "ghndfjh",
                NatinalCode = nationalcode ?? "rwgerge"
            };
            return dto;
        }
    }
}
