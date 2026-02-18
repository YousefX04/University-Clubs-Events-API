namespace ClubManagement.BLL.DTO
{
    public class GetAllEventsDTO
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string ClubName { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
    }
}
