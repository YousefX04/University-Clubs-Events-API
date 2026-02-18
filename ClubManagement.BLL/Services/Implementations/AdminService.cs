using ClubManagement.BLL.DTO;
using ClubManagement.BLL.Services.Interfaces;
using ClubManagement.DAL.Data.Models;
using ClubManagement.DAL.Enums;
using ClubManagement.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ClubManagement.BLL.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public AdminService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task AcceptStatusClub(int clubId)
        {
            var club = await _unitOfWork.Clubs.GetAsync(c => c.Id == clubId);

            if (club == null || club.Status != Status.Pending.ToString())
                throw new Exception("Club id does not exist or its not pending");

            club.Status = Status.Accepted.ToString();

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AcceptStatusEvent(int eventId)
        {
            var _event = await _unitOfWork.Events.GetAsync(e => e.Id == eventId);

            if (_event == null || _event.Status != Status.Pending.ToString())
                throw new Exception("Event id does not exist or its not pending");

            _event.Status = Status.Accepted.ToString();

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AcceptUpdatedClub(int clubId)
        {
            var club = await _unitOfWork.ClubUpdates.GetAsync(c => c.Id == clubId);

            var clubToUpdate = await _unitOfWork.Clubs.GetAsync(c => c.Id == clubId);

            if (club == null)
                throw new Exception("Club id does not exist or its not Updated");

            clubToUpdate.ClubName = club.NewName;
            clubToUpdate.Description = club.NewDescription;

            _unitOfWork.ClubUpdates.Delete(club);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AcceptUpdatedEvent(int eventId)
        {
            var _event = await _unitOfWork.EventUpdates.GetAsync(e => e.Id == eventId);

            var eventToUpdate = await _unitOfWork.Events.GetAsync(e => e.Id == eventId);

            if (_event == null)
                throw new Exception("Club id does not exist or its not Updated");

            eventToUpdate.EventName = _event.NewName;
            eventToUpdate.Description = _event.NewDescription;
            eventToUpdate.StartAt = _event.NewStart ?? eventToUpdate.StartAt;
            eventToUpdate.EndAt = _event.NewEnd ?? eventToUpdate.EndAt;

            _unitOfWork.EventUpdates.Delete(_event);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<PendingUpdatedClubsDTO>> GetAllPedningUpdatedClubs()
        {
            var clubs = await _unitOfWork.ClubUpdates
                .GetAllAsync(
                filter: c => true, // Assuming you want to get all pending updates, you can adjust this filter as needed
                selector: c => new PendingUpdatedClubsDTO
                {
                    Id = c.Id,
                    OldName = c.OldName,
                    NewName = c.NewName,
                    OldDescription = c.OldDescription,
                    NewDescription = c.NewDescription
                });
            return clubs;
        }

        public async Task<List<PendingUpdatedEventsDTO>> GetAllPedningUpdatedEvents()
        {
            var _event = await _unitOfWork.EventUpdates
                .GetAllAsync(
                filter: e => true, // Assuming you want to get all pending updates, you can adjust this filter as needed
                selector: e => new PendingUpdatedEventsDTO
                {
                    Id = e.Id,
                    OldName = e.OldName,
                    NewName = e.NewName,
                    OldDescription = e.OldDescription,
                    NewDescription = e.NewDescription,
                    OldStart = e.OldStart,
                    NewStart = e.NewStart,
                    OldEnd = e.OldEnd,
                    NewEnd = e.NewEnd
                });

            return _event;
        }

        public async Task<List<PendingClubsDTO>> GetAllPendingClubs()
        {
            var clubs = await _unitOfWork.Clubs
                .GetAllAsync(
                filter: c => c.Status == Status.Pending.ToString(),
                selector: c => new PendingClubsDTO
                {
                    Id = c.Id,
                    ClubName = c.ClubName,
                    Description = c.Description,
                    ClubLeaderName = c.User.AppUser.UserName
                });

            return clubs;
        }

        public async Task<List<PendingEventsDTO>> GetAllPendingEvents()
        {
            var events = await _unitOfWork.Events
                .GetAllAsync(
                filter: e => e.Status == Status.Pending.ToString(),
                selector: e => new PendingEventsDTO
                {
                    Id = e.Id,
                    EventName = e.EventName,
                    Description = e.Description,
                    ClubName = e.Club.ClubName,
                    StartAt = e.StartAt,
                    EndAt = e.EndAt
                });

            return events;
        }

        public async Task<AdminDashboardDTO> GetDashboard()
        {
            int countOfPendingClubs = await _unitOfWork.Clubs.CountAsync(c => c.Status == Status.Pending.ToString());
            int countOfAcceptedClubs = await _unitOfWork.Clubs.CountAsync(c => c.Status == Status.Accepted.ToString());
            int countOfPendingEvents = await _unitOfWork.Events.CountAsync(e => e.Status == Status.Pending.ToString());
            int countOfAcceptedEvents = await _unitOfWork.Events.CountAsync(e => e.Status == Status.Accepted.ToString());
            int countOfClubLeaders = await _userManager.GetUsersInRoleAsync("ClubLeader").ContinueWith(t => t.Result.Count);
            int countOfStudents = await _userManager.GetUsersInRoleAsync("Student").ContinueWith(t => t.Result.Count);

            var dashboard = new AdminDashboardDTO()
            {
                NumOfAcceptedClubs = countOfAcceptedClubs,
                NumOfPendingClubs = countOfPendingClubs,
                NumOfAcceptedEvents = countOfAcceptedEvents,
                NumOfPendingEvents = countOfPendingEvents,
                NumOfLeadersClub = countOfClubLeaders,
                NumOfStudents = countOfStudents
            };

            return dashboard;
        }

        public async Task RejectStatusClub(int clubId)
        {
            var club = await _unitOfWork.Clubs.GetAsync(c => c.Id == clubId);

            if (club == null || club.Status != Status.Pending.ToString())
                throw new Exception("Club id does not exist or its not pending");

            club.Status = Status.Rejected.ToString();

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RejectStatusEvent(int eventId)
        {
            var _event = await _unitOfWork.Events.GetAsync(e => e.Id == eventId);

            if (_event == null || _event.Status != Status.Pending.ToString())
                throw new Exception("Event id does not exist or its not pending");

            _event.Status = Status.Rejected.ToString();

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RejectUpdatedClub(int clubId)
        {
            var club = await _unitOfWork.ClubUpdates.GetAsync(c => c.Id == clubId);

            if (club == null)
                throw new Exception("Club Id does not exist or its not Updated");

            _unitOfWork.ClubUpdates.Delete(club);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RejectUpdatedEvent(int eventId)
        {
            var _event = await _unitOfWork.EventUpdates.GetAsync(e => e.Id == eventId);

            if (_event == null)
                throw new Exception("Club id does not exist or its not Updated");

            _unitOfWork.EventUpdates.Delete(_event);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
