using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
