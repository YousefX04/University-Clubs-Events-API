using ClubManagement.DAL.Data;
using ClubManagement.DAL.Data.Models;
using ClubManagement.DAL.Repositories.Interfaces;

namespace ClubManagement.DAL.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Users = new Repository<User>(_db);
            Clubs = new Repository<Club>(_db);
            ClubMembers = new Repository<ClubMember>(_db);
            ClubUpdates = new Repository<ClubUpdate>(_db);
            Events = new Repository<Event>(_db);
            EventMembers = new Repository<EventMember>(_db);
            EventUpdates = new Repository<EventUpdate>(_db);
        }
        public IRepository<User> Users { get; }


        public IRepository<Club> Clubs { get; }

        public IRepository<ClubMember> ClubMembers { get; }

        public IRepository<ClubUpdate> ClubUpdates { get; }

        public IRepository<Event> Events { get; }

        public IRepository<EventMember> EventMembers { get; }

        public IRepository<EventUpdate> EventUpdates { get; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
