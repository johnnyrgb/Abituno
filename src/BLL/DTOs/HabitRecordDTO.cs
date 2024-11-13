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
