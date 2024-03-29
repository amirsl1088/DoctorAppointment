﻿using DoctorAppointment.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistence.EF.Patients
{
    public class PatientEntityMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
           
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.FirstName).IsRequired();
            builder.Property(_ => _.LastName).IsRequired();
            builder.Property(_ => _.NationalCode).IsRequired();
            
        }
    }
}
