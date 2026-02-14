namespace ClubManagement.BLL.DTO
{
    public class PendingUpdatedClubsDTO
    {
        public int Id { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string OldDescription { get; set; }
        public string NewDescription { get; set; }
    }
}
