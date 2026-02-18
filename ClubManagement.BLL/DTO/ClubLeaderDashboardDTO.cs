namespace ClubManagement.BLL.DTO
{
    public class ClubLeaderDashboardDTO
    {
        public string ClubName { get; set; }
        public string ClubStatus { get; set; }
        public int PendingClubMembersCount { get; set; }
        public int AcceptedClubMembersCount { get; set; }
        public int RejectedClubMembersCount { get; set; }
        public int TotalClubMembersCount { get; set; }
        public int PendingEventMembersCount { get; set; }
        public int AcceptedEventMembersCount { get; set; }
        public int RejectedEventMembersCount { get; set; }
        public int AcceptedEventsCount { get; set; }


    }
}
