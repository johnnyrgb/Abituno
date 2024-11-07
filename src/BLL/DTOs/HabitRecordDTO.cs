using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class HabitRecordDTO
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public DateOnly RecordDate { get; set; }

        public HabitRecordDTO() { }
    }
}
