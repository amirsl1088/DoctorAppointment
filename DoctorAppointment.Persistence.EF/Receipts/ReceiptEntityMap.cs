using DoctorAppointment.Entities.Receipts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistence.EF.Receipts
{
    public class ReceiptEntityMap : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> builder)
        {
            builder.HasOne(_ => _.Doctor)
                 .WithMany(_ => _.Receipts);
            builder.HasOne(_ => _.Patient)
                .WithMany(_ => _.Receipts);
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.ReserveDate).IsRequired();
        }
    }
}
