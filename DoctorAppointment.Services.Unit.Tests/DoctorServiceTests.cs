using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Doctors;
using DoctorAppointment.Services.Doctors;
using DoctorAppointment.Services.Doctors.Contracts.Dto;
using DoctorAppointment.Services.Doctors.Exceptions;
using DoctorAppointment.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using FluentAssertions;

namespace DoctorAppointment.Services.Unit.Tests;

public class DoctorServiceTests
{
    [Fact]
    public async Task Add_adds_a_new_doctor_properly()
    {
        //arrange
        var dto = new AddDoctorDto
        {
            FirstName = "dummy-first-name",
            LastName = "dummy-last-name",
            Field = "heart",
            NationalCode = "3545345"

        };
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));

        //act
        await sut.Add(dto);

        //assert
        var actual = readContext.Doctors.Single();
        actual.FirstName.Should().Be(dto.FirstName);
        actual.LastName.Should().Be(dto.LastName);
        actual.Field.Should().Be(dto.Field);
        actual.NationalCode.Should().Be(dto.NationalCode);
    }

    [Fact]
    public async Task Update_updates_doctor_properly()
    {
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        //arrange
        var doctor = new Doctor
        {
            FirstName = "dummy-first-name",
            LastName = "dummy-last-name",
            Field = "heart",
            NationalCode = "4645774"

        };
        context.Save(doctor);
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        var updateDto = new UpdateDoctorDto
        {
            FirstName = "updated-dummy-first-name",
            LastName = "updated-dummy-last-name",
            Field = "child",
            NationalCode = "78687547"

        };

        //act
        await sut.Update(doctor.Id, updateDto);

        //assert
        var actual = readContext.Doctors.First(_ => _.Id == doctor.Id);
        actual.FirstName.Should().Be(updateDto.FirstName);
        actual.LastName.Should().Be(updateDto.LastName);
        actual.Field.Should().Be(updateDto.Field);
        actual.NationalCode.Should().Be(updateDto.NationalCode);
    }
    [Fact]
    public async Task Update_throws_exeption_when_doctors_id_not_found()
    {
        var id = 8;
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();

        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        var updateDto = new UpdateDoctorDto
        {
            FirstName = "updated-dummy-first-name",
            LastName = "updated-dummy-last-name",
            Field = "public",
            NationalCode = "23453635"

        };

        var actual = () => sut.Update(id, updateDto);

        await actual.Should().ThrowExactlyAsync<DoctorIdNotFound>();

    }
    [Fact]
    public async Task Add_throws_exeption_when_doctors_have_same_nationalcode()
    {
        var dto = new AddDoctorDto
        {
            FirstName = "dummy-first-name",
            LastName = "dummy-last-name",
            Field = "heart",
            NationalCode = "3545345"

        };
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        await sut.Add(dto);
        var dto2 = new AddDoctorDto
        {
            FirstName = "sdfadf",
            LastName = "afadg",
            Field = "deffe",
            NationalCode = "3545345"
        };

        var actual = () => sut.Add(dto2);

        await actual.Should().ThrowExactlyAsync<DoctorsCannotHaveSameNationalCode>();

    }
    [Fact]
    public void Get_gets_count_of_doctors_properly()
    {
        var dto = new AddDoctorDto
        {
            FirstName = "adfvsdv",
            LastName = "sgvsrg",
            Field = "public",
            NationalCode = "3545345003"

        };
        var dto2 = new AddDoctorDto
        {
            FirstName = "sdgadfvsdv",
            LastName = "sgvsasegfrg",
            Field = "public33",
            NationalCode = "3sge545345003"

        };
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        var exepted = 2;

        _ = sut.Add(dto);
        sut.Add(dto2);

        var actual = readContext.Doctors.Count();
        actual.Should().Be(exepted);

    }
    [Fact]
    public void Get_gets_information_of_doctors_properly()
    {
      
      
        var dto = new AddDoctorDto
        {
            FirstName = "mehdi",
            LastName = "ahmadi",
            Field = "heart",
            NationalCode = "1234"
        };
       
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        

        
        sut.Add(dto);


        var actual = readContext.Doctors.First();
        actual.FirstName.Equals(dto.FirstName);
        actual.LastName.Equals(dto.LastName);
        actual.Field.Equals(dto.Field);
        actual.NationalCode.Equals(dto.NationalCode);
        
    }
    [Fact]
    public void Delete_delete_doctor_from_table_doctors_properly()
    {
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));
        var expected = false;
        var doctor = new Doctor
        {
            FirstName = "efsegs",
            LastName = "adgwrh",
            Field = "heart",
            NationalCode = "344436545345"

        };
        context.Save(doctor);

        _ = sut.Delete(doctor.Id);

        var actual = readContext.Doctors.Any(_ => _.Id == doctor.Id);
        actual.Should().Be(expected);
    }
    [Fact]
    public async Task Delete_throws_exeption_when_doctorid_not_found()
    {
        var id = 8;
        var db = new EFInMemoryDatabase();
        var context = db.CreateDataContext<EFDataContext>();
        var readContext = db.CreateDataContext<EFDataContext>();
        var sut = new DoctorAppService(new EFDoctorRepository(context), new EFUnitOfWork(context));

        var actual = () => sut.Delete(id);

        await actual.Should().ThrowExactlyAsync<DoctorIdNotFound>();


    }
}










