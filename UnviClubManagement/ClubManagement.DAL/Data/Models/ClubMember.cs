using System.ComponentModel.DataAnnotations.Schema;

namespace ClubManagement.DAL.Data.Models
{
    public class ClubMember
    {
        [ForeignKey("Club")]
        public int? ClubID { get; set; }

        public Club? Club { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public User User { get; set; }

        public string Status { get; set; }
    }
}
