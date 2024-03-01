using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Migrations.Doctors
{
    [Migration(202402291722)]
    public class _202402291722_AddedDoctorsTable : Migration
    {

        public override void Up()
        {

            Create.Table("Doctors")
                 .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                 .WithColumn("FirstName").AsString(50).NotNullable()
                 .WithColumn("LastName").AsString(50).NotNullable()
                 .WithColumn("Field").AsString(50).NotNullable()
                 .WithColumn("NationalCode").AsString(25).NotNullable();
        }
        public override void Down()
        {
            Delete.Table("Doctors");
        }
    }
}
