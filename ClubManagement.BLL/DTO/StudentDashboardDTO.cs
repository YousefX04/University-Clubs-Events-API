namespace ClubManagement.BLL.DTO
{
    public class StudentDashboardDTO
    {
        public int NumOfJoinedClubs { get; set; }
        public int NumOfJoinedEvents { get; set; }

        public List<ClubJoinedDTO> JoinedClubs { get; set; } = new List<ClubJoinedDTO>();
        public List<EventJoinedDTO> JoinedEvents { get; set; } = new List<EventJoinedDTO>();
    }
}
