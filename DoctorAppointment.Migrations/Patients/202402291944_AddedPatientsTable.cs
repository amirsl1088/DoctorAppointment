﻿using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Migrations.Patients
{
    [Migration(202402291944)]
    public class _202402291944_AddedPatientsTable : Migration
    {

        public override void Up()
        {
            Create.Table("Patients")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(50).NotNullable()
                .WithColumn("NationalCode").AsString(25).NotNullable();
                
        }
        public override void Down()
        {
            Delete.Table("Patients");
        }
    }
}
