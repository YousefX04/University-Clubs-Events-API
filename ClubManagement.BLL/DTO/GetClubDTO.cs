namespace ClubManagement.BLL.DTO
{
    public class GetClubDTO
    {
        public int Id { get; set; }
        public string ClubName { get; set; }
        public string Description { get; set; }
        public string ClubLeaderName { get; set; }
        public List<GetClubMemeberDTO> Members { get; set; }
    }
}
