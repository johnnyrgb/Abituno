namespace DAL.Entities
{
    public partial class HabitRecord
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public DateOnly RecordDate { get; set; }
    }
}
