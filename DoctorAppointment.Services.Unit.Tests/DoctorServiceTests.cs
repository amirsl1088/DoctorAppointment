using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Doctors;
using DoctorAppointment.Services.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Services.Doctors.Contracts.Dto;
using DoctorAppointment.Services.Doctors.Exceptions;
using DoctorAppointment.Test.Tools.Doctors;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests;

public class DoctorServiceTests
{

    private readonly EFDataContext _context;
    private readonly EFDataContext _readContext;
    private readonly DoctorService _sut;
    public DoctorServiceTests()
    {
        var db = new EFInMemoryDatabase();
        _context = db.CreateDataContext<EFDataContext>();
        _readContext = db.CreateDataContext<EFDataContext>();
        _sut = DoctorServiceFactory.Create(_context);
    }
    [Fact]
    public async Task Add_adds_a_new_doctor_properly()
    {
        //arrange
        var dto = AddDoctorDtoFactory.Create("wegfwg");

        //act
        await _sut.Add(dto);

        //assert
        var actual = _readContext.Doctors.Single();
        actual.FirstName.Should().Be(dto.FirstName);
        actual.LastName.Should().Be(dto.LastName);
        actual.Field.Should().Be(dto.Field);
        actual.NationalCode.Should().Be(dto.NationalCode);
    }

    [Fact]
    public async Task Update_updates_doctor_properly()
    {
        //arrange
        var doctor = new DoctorBuilder()
            .Build();
        _context.Save(doctor);
        var updateDto = new UpdateDoctorDto
        {
            FirstName = "updated-dummy-first-name",
            LastName = "updated-dummy-last-name",
            Field = "child",
            NationalCode = "78687547"

        };

        //act
        await _sut.Update(doctor.Id, updateDto);

        //assert
        var actual = _readContext.Doctors.First(_ => _.Id == doctor.Id);
        actual.FirstName.Should().Be(updateDto.FirstName);
        actual.LastName.Should().Be(updateDto.LastName);
        actual.Field.Should().Be(updateDto.Field);
        actual.NationalCode.Should().Be(updateDto.NationalCode);
    }
    [Fact]
    public async Task Update_throws_exeption_when_doctors_id_not_found()
    {
        var dummyid = 8;
        var updateDto = new UpdateDoctorDto
        {
            FirstName = "updated-dummy-first-name",
            LastName = "updated-dummy-last-name",
            Field = "public",
            NationalCode = "23453635"

        };

        var actual = () => _sut.Update(dummyid, updateDto);

        await actual.Should().ThrowExactlyAsync<DoctorIdNotFound>();

    }
    [Fact]
    public async Task Add_throws_exeption_when_doctors_have_same_nationalcode()
    {
        var doctor = new DoctorBuilder()
            .WithNationalCode("3545345")
            .Build();
        _context.Save(doctor);
        var dto2 = AddDoctorDtoFactory.Create("3545345");

        var actual = () => _sut.Add(dto2);

        await actual.Should().ThrowExactlyAsync<DoctorsCannotHaveSameNationalCode>();

    }
    [Fact]
    public async void Get_gets_count_of_doctors_properly()
    {
        var doctor1 = new DoctorBuilder()
            .Build();
        var doctor2 = new DoctorBuilder()
            .WithNationalCode("21542624")
            .Build();
        var exepted = 2;
        _context.Save(doctor1);
        _context.Save(doctor2);

        var actual = await _sut.GetDoctors();

        actual.Count().Should().Be(exepted);

    }
    [Fact]
    public async Task Get_gets_information_of_doctors_properly()
    {


        var doctor = new DoctorBuilder()
            .WithFirstName("oso")
            .Build();
        _context.Save(doctor);

        var actual = await _sut.GetDoctors();

        actual.First().NationalCode.Should().Be(doctor.NationalCode);
        actual.First().Id.Should().Be(doctor.Id);
        actual.First().LastName.Should().Be(doctor.LastName);
        actual.First().Field.Should().Be(doctor.Field);
        actual.First().FirstName.Should().Be(doctor.FirstName);
    }
    [Fact]
    public void Delete_delete_doctor_from_table_doctors_properly()
    {
        var expected = false;
        var doctor = new DoctorBuilder()
            .WithFirstName("ssss")
            .Build();
        _context.Save(doctor);

        _sut.Delete(doctor.Id);

        var actual = _readContext.Doctors.Any(_ => _.Id == doctor.Id);
        actual.Should().Be(expected);
    }
    [Fact]
    public async Task Delete_throws_exeption_when_doctorid_not_found()
    {
        var dummyid = 8;

        var actual = () => _sut.Delete(dummyid);

        await actual.Should().ThrowExactlyAsync<DoctorIdNotFound>();


    }
}










