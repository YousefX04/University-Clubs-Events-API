using ClubManagement.BLL.DTO;

namespace ClubManagement.BLL.Services.Interfaces
{
    public interface IAdminService
    {
        Task<List<PendingClubsDTO>> GetAllPendingClubs();
        Task AcceptStatusClub(int clubId);
        Task RejectStatusClub(int clubId);
        Task<List<PendingUpdatedClubsDTO>> GetAllPedningUpdatedClubs();
        Task AcceptUpdatedClub(int clubId);
        Task RejectUpdatedClub(int clubId);
        Task<List<PendingEventsDTO>> GetAllPendingEvents();
        Task AcceptStatusEvent(int eventId);
        Task RejectStatusEvent(int eventId);
        Task<List<PendingUpdatedEventsDTO>> GetAllPedningUpdatedEvents();
        Task AcceptUpdatedEvent(int eventId);
        Task RejectUpdatedEvent(int eventId);
        Task<AdminDashboardDTO> GetDashboard();
    }
}
