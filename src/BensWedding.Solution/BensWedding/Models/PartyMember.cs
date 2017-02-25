using System.ComponentModel.DataAnnotations;

namespace BensWedding.Models
{
    public class PartyMember
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Order { get; set; }
    }
}