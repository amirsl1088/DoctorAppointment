using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Doctors;
using DoctorAppointment.Services.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Exceptions;
using DoctorAppointment.Services.Patients;
using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Services.Patients.Contracts.Dto;
using DoctorAppointment.Services.Patients.Exeptions;
using DoctorAppointment.Test.Tools.Doctors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using DoctorAppointment.Test.Tools.Patients;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Unit.Tests.Patients
{
    public class PatientServiceTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly PatientService _sut;
        public PatientServiceTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = PatientServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Add_adds_patient_peroperly()
        {
            var dto = AddPatientDtoFactory.Create();

            await _sut.Add(dto);

            var actual = _readContext.Patients.Single();
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);
            actual.NationalCode.Should().Be(dto.NatinalCode);
        }
        [Fact]
        public async Task Add_throws_exeption_when_patients_have_same_nationacode()
        {
            var patient = new PatientBuilder()
                .WithNationalCode("1234")
                .Build();
            _context.Save(patient);
            var dto2 = AddPatientDtoFactory.Create("1234");

            var actual = () => _sut.Add(dto2);

            await actual.Should().ThrowExactlyAsync<PatientsCannotHaveSameNationalCode>();
        }
        [Fact]
        public async Task Update_updates_patients_imformations_properly()
        {

            var patient = new PatientBuilder().Build();
            var updateDto = new UpdatePatientDto
            {
                FirstName = "sdfffwgdg",
                LastName = "rwgwergadsf",
                NationalCode = "wrgwrgwegg"
            };
            _context.Save(patient);

            await _sut.Update(patient.Id, updateDto);

            var actual = _readContext.Patients.First(_ => _.Id == patient.Id);
            actual.FirstName.Should().Be(updateDto.FirstName);
            actual.LastName.Should().Be(updateDto.LastName);
            actual.NationalCode.Should().Be(updateDto.NationalCode);
        }
        [Fact]
        public async Task Update_throws_exeption_when_Patient_id_not_found()
        {
            var id = 4;

            var updateDto = new UpdatePatientDto
            {
                FirstName = "efsrg",
                LastName = "wefweg",
                NationalCode = "wgwgwrg"
            };

            var actual = () => _sut.Update(id, updateDto);

            await actual.Should().ThrowExactlyAsync<PatientNotFound>();
        }
        [Fact]
        public async Task Get_gets_count_of_patients_properly()
        {
            var patient = new PatientBuilder().Build();
            var patient2 = new PatientBuilder().Build();
            var expected = 2;
            _context.Save(patient);
            _context.Save(patient2);

            var actual =await _sut.GetPatients();

            actual.Count().Should().Be(expected);
        }
        [Fact]
        public async Task Get_gets_imformation_of_patients_properly()
        {
            var patient = new PatientBuilder().Build();
            _context.Save(patient);

            var actual =await _sut.GetPatients();

            actual.First().FirstName.Should().Be(patient.FirstName);
            actual.First().LastName.Should().Be(patient.LastName);
            actual.First().NationalCode.Should().Be(patient.NationalCode);


        }
        [Fact]
        public void Delete_delete_patient_from_table_patients_properly()
        {

            var exepted = false;
            var patient = new PatientBuilder().Build();
            _context.Save(patient);

            _sut.Delete(patient.Id);

            var actual = _readContext.Patients.Any(_ => _.Id == patient.Id);
            actual.Should().Be(exepted);
        }
        [Fact]
        public async Task Delete_throws_exeption_when_patientid_not_found_exeption()
        {
            var dummyid = 5;

            var actual = () => _sut.Delete(dummyid);

            await actual.Should().ThrowExactlyAsync<PatientNotFound>();
        }
    }
}
