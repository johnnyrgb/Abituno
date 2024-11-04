using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public partial class Habit
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public virtual ICollection<HabitRecord> HabitRecords { get; set; } = new List<HabitRecord>();
    }
}
