using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Migrations.Receipts
{
    [Migration(202403020215)]
    public class _202403020215_AddedReceiptsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Receipts")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("DoctorName").AsString(50).NotNullable()
                .WithColumn("PatientName").AsString(50).NotNullable()
                .WithColumn("ReserveDate").AsDateTime().NotNullable()
                .WithColumn("DoctorId").AsInt32().ForeignKey("FK_Receipts_Doctors", "Doctors", "Id")
                .WithColumn("PatientId").AsInt32().ForeignKey("FK_Receipts_Patients", "Patients", "Id");
        }
        public override void Down()
        {
            Delete.Table("Receipts");
        }

    }
}
