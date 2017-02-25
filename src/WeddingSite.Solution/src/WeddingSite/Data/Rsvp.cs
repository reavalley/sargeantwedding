using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeddingSite.Data.Users;

namespace WeddingSite.Data
{
    public class Rsvp
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public Attending Attending { get; set; }

        [Required]
        public bool IsCamping { get; set; }

        [Required]
        public MenuOption MenuOption { get; set; }

        [ForeignKey("UserId")]
        [Required]
        public virtual ApplicationUser User { get; set; }
    }    
}
