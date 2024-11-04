using DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class HabitRecordConfiguration : IEntityTypeConfiguration<HabitRecord>
    {
        public void Configure(EntityTypeBuilder<HabitRecord> builder)
        {
            builder.HasKey(hr => hr.Id); // Первичный ключ

            builder.Property(hr => hr.RecordDate)
                .IsRequired();

            // Настройка внешнего ключа
            builder.HasOne<Habit>()
                .WithMany()
                .HasForeignKey(hr => hr.HabitId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
