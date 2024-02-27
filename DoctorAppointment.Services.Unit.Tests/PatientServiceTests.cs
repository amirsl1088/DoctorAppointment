using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Doctors;
using DoctorAppointment.Services.Doctors;
using DoctorAppointment.Services.Doctors.Exceptions;
using DoctorAppointment.Services.Patients;
using DoctorAppointment.Services.Patients.Contracts.Dto;
using DoctorAppointment.Services.Patients.Exeptions;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Unit.Tests
{
    public class PatientServiceTests
    {
        [Fact]
        public async Task Add_adds_patient_peroperly()
        {
            var dto = new AddPatientDto
            {
                FirstName = "htdtjg",
                LastName = "ghndfjh",
                NatinalCode = "dfaeggw"
            };
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new PatientAppService(new EFPatientRepository(context), new EFUnitOfWork(context));

            await sut.Add(dto);

            var actual = readContext.Patients.Single();
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);
            actual.NationalCode.Should().Be(dto.NatinalCode);
        }
        [Fact]
        public async Task Add_throws_exeption_when_patients_have_same_nationacode()
        {
            var dto = new AddPatientDto
            {
                FirstName = "jtdvnm",
                LastName = "kughkjl",
                NatinalCode = "35465313"
            };
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new PatientAppService(new EFPatientRepository(context), new EFUnitOfWork(context));
            await sut.Add(dto);
            var dto2 = new AddPatientDto
            {
                FirstName = "asfwef3",
                LastName = "eagwrg",
                NatinalCode = "35465313"
            };

            var actual = () => sut.Add(dto2);

            await actual.Should().ThrowExactlyAsync<PatientsCannotHaveSameNationalCode>();
        }
        [Fact]
        public async Task Update_updates_patients_imformations_properly()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var patient = new Patient
            {
                FirstName = "sdfffw",
                LastName = "rwgwerg",
                NationalCode = "wrgwrg"
            };
            context.Save(patient);
            var sut = new PatientAppService(new EFPatientRepository(context), new EFUnitOfWork(context));
            var updateDto = new UpdatePatientDto
            {
                FirstName = "sdfffwgdg",
                LastName = "rwgwergadsf",
                NationalCode = "wrgwrgwegg"
            };
            await sut.Update(patient.Id, updateDto);

            var actual = readContext.Patients.First(_ => _.Id == patient.Id);
            actual.FirstName.Should().Be(updateDto.FirstName);
            actual.LastName.Should().Be(updateDto.LastName);
            actual.NationalCode.Should().Be(updateDto.NationalCode);
        }
        [Fact]
        public async Task Update_throws_exeption_when_Patient_id_not_found()
        {
            var id = 4;
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new PatientAppService(new EFPatientRepository(context), new EFUnitOfWork(context));
            var updateDto = new UpdatePatientDto
            {
                FirstName = "efsrg",
                LastName = "wefweg",
                NationalCode = "wgwgwrg"
            };

            var actual = () => sut.Update(id, updateDto);

            await actual.Should().ThrowExactlyAsync<PatientNotFound>();
        }
        [Fact]
        public void Get_gets_count_of_patients_properly()
        {
            var dto = new AddPatientDto
            {
                FirstName = "fdfgwf",
                LastName = "egwrfw",
                NatinalCode = "fwrggbbgbg"
            };
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new PatientAppService(new EFPatientRepository(context), new EFUnitOfWork(context));
            var expected = 1;

            _ = sut.Add(dto);

            var actual = readContext.Patients.Count();
            actual.Should().Be(expected);
        }
        [Fact]
        public void Get_gets_imformation_of_patients_properly()
        {
            var dto = new AddPatientDto
            {
                FirstName = "ali",
                LastName = "ahmadi",
                NatinalCode = "1234"
            };
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new PatientAppService(new EFPatientRepository(context), new EFUnitOfWork(context));

            _ = sut.Add(dto);

            var actual = readContext.Patients.First();
            actual.FirstName.Should().Be("ali");
            actual.LastName.Should().Be("ahmadi");
            actual.NationalCode.Should().Be("1234");
            actual.Id.Equals(1);
        }
        [Fact]
        public void Delete_delete_patient_from_table_patients_properly()
        {
            var db = new EFInMemoryDatabase();
            var context = db.CreateDataContext<EFDataContext>();
            var readContext = db.CreateDataContext<EFDataContext>();
            var sut = new PatientAppService(new EFPatientRepository(context), new EFUnitOfWork(context));
            var exepted = false;
            var patient = new Patient
            {
                FirstName = "fserf",
                LastName = "wrggt",
                NationalCode = "wrfwrgg"
            };
            context.Save(patient);

            sut.Delete(patient.Id);

            var actual = readContext.Patients.Any(_ => _.Id == patient.Id);
            actual.Should().Be(exepted);
        }
    }
}
