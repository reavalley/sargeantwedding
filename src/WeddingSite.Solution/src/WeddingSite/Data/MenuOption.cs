using System.ComponentModel.DataAnnotations;

namespace WeddingSite.Data
{
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
}
