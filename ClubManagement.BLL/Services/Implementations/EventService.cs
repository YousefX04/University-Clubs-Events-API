using ClubManagement.BLL.DTO;
using ClubManagement.BLL.Services.Interfaces;
using ClubManagement.DAL.Data.Models;
using ClubManagement.DAL.Enums;
using ClubManagement.DAL.Repositories.Interfaces;
using FluentValidation;

namespace ClubManagement.BLL.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddEventDTO> _addValidator;
        private readonly IValidator<UpdateEventDTO> _updateValidator;


        public EventService(IUnitOfWork unitOfWork, IValidator<AddEventDTO> addValidator, IValidator<UpdateEventDTO> updateValidator)
        {
            _unitOfWork = unitOfWork;
            _addValidator = addValidator;
            _updateValidator = updateValidator;
        }

        public async Task AddEvent(AddEventDTO model)
        {
            var result = _addValidator.Validate(model);
            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var eventName = await _unitOfWork.Events.GetAsync(e => e.EventName == model.EventName);

            if (eventName != null)
                throw new Exception("Name Already Exist");


            var newEvent = new Event()
            {
                ClubID = model.ClubID,
                EventName = model.EventName,
                Description = model.Desc,
                StartAt = model.StartAt,
                EndAt = model.EndAt,
                Status = Status.Pending.ToString()
            };

            await _unitOfWork.Events.AddAsync(newEvent);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEvent(int eventId)
        {
            var _event = await _unitOfWork.Events.GetAsync(e => e.Id == eventId);

            if (_event == null)
                throw new Exception("Event Id Does Not Exist");


            await _unitOfWork.EventMembers.DeleteWhereAsync(em => em.EventID == eventId);


            _unitOfWork.Events.Delete(_event);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<GetAllEventsDTO>> GetAllEvents(int clubId)
        {
            var events = await _unitOfWork.Events
                .GetAllAsync(
                filter: e => e.Status == Status.Accepted.ToString() && e.ClubID == clubId,
                selector: e => new GetAllEventsDTO
                {
                    Id = e.Id,
                    EventName = e.EventName,
                    EventDescription = e.Description,
                    ClubName = e.Club.ClubName,
                    StartAt = e.StartAt,
                    EndAt = e.EndAt
                });

            return events;
        }

        public async Task<List<GetAllEventsDTO>> GetAllEvents()
        {
            var events = await _unitOfWork.Events
                .GetAllAsync(
                filter: e => e.Status == Status.Accepted.ToString(),
                selector: e => new GetAllEventsDTO
                {
                    Id = e.Id,
                    EventName = e.EventName,
                    EventDescription = e.Description,
                    ClubName = e.Club.ClubName,
                    StartAt = e.StartAt,
                    EndAt = e.EndAt
                });

            return events;
        }

        public async Task JoinEvent(int studentId, int eventId)
        {
            await _unitOfWork.EventMembers.AddAsync(new EventMember
            {
                EventID = eventId,
                UserID = studentId,
                Status = Status.Pending.ToString()
            });

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateEvent(UpdateEventDTO model)
        {
            var result = _updateValidator.Validate(model);
            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var _event = await _unitOfWork.Events.GetAsync(e => e.Id == model.EventID);

            if (_event == null)
                throw new Exception("Event ID Does not exist");

            var eventName = await _unitOfWork.Events.GetAsync(e => e.EventName == model.EventName);

            if (eventName != null)
                throw new Exception("Name Already Exist");

            var updatedEvent = new EventUpdate
            {
                Id = model.EventID,
                OldName = _event.EventName,
                NewName = model.EventName,
                OldDescription = _event.Description,
                NewDescription = model.Desc,
                OldStart = _event.StartAt,
                NewStart = model.StartAt,
                OldEnd = _event.EndAt,
                NewEnd = model.EndAt,
            };

            await _unitOfWork.EventUpdates.AddAsync(updatedEvent);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
