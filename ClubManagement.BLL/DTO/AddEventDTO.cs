namespace ClubManagement.BLL.DTO
{
    public class AddEventDTO
    {

        public string EventName { get; set; }


        public string Desc { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public int ClubID { get; set; }
    }
}
