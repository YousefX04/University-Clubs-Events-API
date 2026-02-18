using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubManagement.DAL.Data.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; } // for Identity
        public AppUser AppUser { get; set; } // for Identity

        public Club Club { get; set; } // for clubleader

        public ICollection<ClubMember> JoinedClubs { get; set; } = new List<ClubMember>();

        public ICollection<EventMember> JoinedEvents { get; set; } = new List<EventMember>();

    }
}
