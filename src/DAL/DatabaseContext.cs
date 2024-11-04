using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitRecord> HabitRecords { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.HabitConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.HabitRecordConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}