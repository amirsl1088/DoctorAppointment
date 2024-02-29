using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Patients
{
    public class PatientBuilder
    {
        private readonly Patient _patient;
        public PatientBuilder()
        {
            _patient = new Patient
            {
                FirstName = "sdfffw",
                LastName = "rwgwerg",
                NationalCode = "wrgwrg"
            };
        }
        public PatientBuilder WithFirstName(string firstname)
        {
            _patient.FirstName = firstname;
            return this;
        }
        public PatientBuilder WithLastName(string lastname)
        {
            _patient.LastName = lastname;
            return this;
        }
        public PatientBuilder WithNationalCode(string nationalcode)
        {
            _patient.NationalCode = nationalcode;
            return this;
        }
        public Patient Build()
        {
            return _patient;
        }
    }
   
}
