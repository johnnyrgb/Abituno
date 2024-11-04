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
    public class HabitConfiguration : IEntityTypeConfiguration<Habit>
    {
        public void Configure(EntityTypeBuilder<Habit> builder)
        {
            builder.HasKey(h => h.Id); // Первичный ключ

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(h => h.StartDate)
                .IsRequired();

            builder.Property(h => h.EndDate)
                .IsRequired();
        }
    }
}
