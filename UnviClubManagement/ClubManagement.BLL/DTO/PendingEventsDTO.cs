namespace ClubManagement.BLL.DTO
{
    public class PendingEventsDTO
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string ClubName { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

    }
}
