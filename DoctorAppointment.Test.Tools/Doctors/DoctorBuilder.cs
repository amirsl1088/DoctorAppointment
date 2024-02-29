using DoctorAppointment.Entities.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Test.Tools.Doctors
{
    public class DoctorBuilder
    {
        private readonly Doctor _doctor;
        public DoctorBuilder()
        {
            _doctor = new Doctor
            {
                FirstName = "mehdi",
                LastName = "ahmadi",
                Field = "heart",
                NationalCode = "1234"
            };
        }
        public DoctorBuilder WithFirstName(string firstname)
        {
            _doctor.FirstName = firstname;
            return this;
        }
        public DoctorBuilder WithLastName(string lastname)
        {
            _doctor.LastName = lastname;
            return this;
        }
        public DoctorBuilder WithNationalCode(string nationalcode)
        {
            _doctor.NationalCode = nationalcode;
            return this;
        }
        public Doctor Build()
        {
            return _doctor;
        }
    }
}
