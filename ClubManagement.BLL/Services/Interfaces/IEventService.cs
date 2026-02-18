using ClubManagement.BLL.DTO;

namespace ClubManagement.BLL.Services.Interfaces
{
    public interface IEventService
    {
        Task AddEvent(AddEventDTO model);
        Task DeleteEvent(int eventId);
        Task UpdateEvent(UpdateEventDTO model);
        Task<List<GetAllEventsDTO>> GetAllEvents(int clubId);
        Task<List<GetAllEventsDTO>> GetAllEvents();
        Task JoinEvent(int studentId, int eventId);

    }
}
