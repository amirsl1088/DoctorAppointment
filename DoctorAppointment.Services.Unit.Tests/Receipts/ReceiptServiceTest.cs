using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Test.Tools.Doctors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using DoctorAppointment.Test.Tools.Patients;
using FluentAssertions;
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
            var dto = new AddReceiptDto
            {
                PatientName = "dagfsb",
                DoctorName = "ewgwgwrg",
                ReserveDate = DateTime.Now,
                DoctorId = doctor.Id,
                PatientId=patient.Id
            };
            _context.Save(doctor);
            _context.Save(patient);

            await _sut.Add(dto);

            var actual = _readContext.Receipts.Single();
            actual.PatientName.Should().Be(dto.PatientName);
            actual.DoctorName.Should().Be(dto.DoctorName);
            actual.ReserveDate.Should().Be(dto.ReserveDate);
            actual.DoctorId.Should().Be(dto.DoctorId);
            actual.PatientId.Should().Be(dto.PatientId);
        }
    }
}
