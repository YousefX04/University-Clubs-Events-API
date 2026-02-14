namespace ClubManagement.BLL.DTO
{
    public class PendingUpdatedEventsDTO
    {
        public int Id { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string OldDescription { get; set; }
        public string NewDescription { get; set; }
        public DateTime? OldStart { get; set; }
        public DateTime? NewStart { get; set; }
        public DateTime? OldEnd { get; set; }
        public DateTime? NewEnd { get; set; }
    }
}
