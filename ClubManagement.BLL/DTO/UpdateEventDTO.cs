namespace ClubManagement.BLL.DTO
{
    public class UpdateEventDTO
    {
        public string EventName { get; set; }

        public string Desc { get; set; }

        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }

        public int EventID { get; set; }
    }
}
