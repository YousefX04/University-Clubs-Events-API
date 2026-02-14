namespace ClubManagement.BLL.DTO
{
    public class AddClubDTO
    {
        public string ClubName { get; set; }

        public string Desc { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int ClubLeaderID { get; set; }
    }
}
