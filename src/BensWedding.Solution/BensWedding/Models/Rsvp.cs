using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BensWedding.Models
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

        public string DietaryRequirements { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
    }

    public class MenuOption
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class Attending
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}