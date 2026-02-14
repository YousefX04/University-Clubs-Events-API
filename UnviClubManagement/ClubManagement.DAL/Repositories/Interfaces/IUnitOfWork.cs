using ClubManagement.DAL.Data.Models;

namespace ClubManagement.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Club> Clubs { get; }
        IRepository<ClubMember> ClubMembers { get; }
        IRepository<ClubUpdate> ClubUpdates { get; }
        IRepository<Event> Events { get; }
        IRepository<EventMember> EventMembers { get; }
        IRepository<EventUpdate> EventUpdates { get; }


        Task<int> SaveChangesAsync();

    }
}
