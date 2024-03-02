using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Exceptions;
using DoctorAppointment.Services.Receipts.Exceptions;
using DoctorAppointment.Test.Tools.Doctors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using DoctorAppointment.Test.Tools.Patients;
using DoctorAppointment.Test.Tools.Receipts;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Unit.Tests.Receipts
{
    public class ReceiptServiceTest
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly ReceiptService _sut;
        public ReceiptServiceTest()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = new ReceiptAppService(new EFReceiptRepository(_context), new EFUnitOfWork(_context));
        }
        [Fact]
        public async Task Add_adds_receipt_for_patients_properly()
        {
            var doctor = new DoctorBuilder()
                .WithFirstName("ewgwgwrg")
                .Build();
            var patient = new PatientBuilder()
                .WithFirstName("dagfsb")
                .Build();
            var dto = AddReceiptDtoFactory.Create();
            _context.Save(doctor);
            _context.Save(patient);

            await _sut.Add(dto);

            var actual = _readContext.Receipts.Single();
            actual.ReserveDate.Should().Be(dto.ReserveDate);
            actual.DoctorId.Should().Be(doctor.Id);
            actual.PatientId.Should().Be(patient.Id);
        }
        [Fact]
        public async Task Add_throws_exception_when_doctorid_not_found_exception()
        {
            var patient = new PatientBuilder().Build();
            _context.Save(patient);
            var dto = AddReceiptDtoFactory.Create();

          var actual=async ()=>await _sut.Add(dto);

           await actual.Should().ThrowExactlyAsync<DoctorIdNotFoundException>();

           
        }
        [Fact]
        public async Task Add_throws_exception_when_patientid_not_found_exception()
        {
            var doctor = new DoctorBuilder().Build();
            _context.Save(doctor);
            var dto = AddReceiptDtoFactory.Create();

            var actual = async () => await _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<PatientIdNotFoundException>();
        }
        [Fact]
        public async Task Add_throws_exeption_when_datetime_is_passed_exception()
        {
            var dummydatetime = new DateTime(2019, 02, 02);
            var patient = new PatientBuilder().Build();
            var doctor = new DoctorBuilder().Build();
            var dto = AddReceiptDtoFactory.Create(dummydatetime);
            _context.Save(patient);
            _context.Save(doctor);

            var actual = async () => await _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<DateTimePassedException>();
        }
        [Fact]
        public async Task Get_gets_receipt_doctor_with_patient_properly()
        {
            var doctor = new DoctorBuilder().Build();
            var patient = new PatientBuilder().Build();
            var receipt = new ReceiptBuilder().Build();
            _context.Save(doctor);
            _context.Save(patient);
            _context.Save(receipt);
            var dto = GetReceiptDtoFactory.Create();

           await _sut.GetAll();

            var actual = _readContext.Receipts.Single();
            actual.Id.Should().Be(dto.Id);
            actual.DoctorId.Should().Be(dto.DoctorId);
            actual.PatientId.Should().Be(dto.PatientId);
            actual.ReserveDate.Should().Be(dto.ReserveDate);
        }
        [Fact]
        public async Task Update_updates_receipt_date_properly()
        {
            var doctor = new DoctorBuilder().Build();
            var patient = new PatientBuilder().Build();
            var receipt = new ReceiptBuilder().Build();
            _context.Save(doctor);
            _context.Save(patient);
            _context.Save(receipt);
            var dto = UpdateReceiptDtoFactory.Create(new DateTime(2024,03,05));

            await _sut.Update(receipt.Id,dto);

            var actual = _readContext.Receipts.Single();
            actual.ReserveDate.Should().Be(dto.ReserveDate);
        }
        [Fact]
        public async Task Delete_deletes_receipt_from_table_receipts_properly()
        {
            var doctor = new DoctorBuilder().Build();
            var patient = new PatientBuilder().Build();
            var receipt = new ReceiptBuilder().Build();
            _context.Save(doctor);
            _context.Save(patient);
            _context.Save(receipt);

            await _sut.Delete(receipt.Id);

            var actual =await _readContext.Receipts.FirstOrDefaultAsync(_ => _.Id == receipt.Id);
             actual.Should().Be(null);
        }
      
    }
}
