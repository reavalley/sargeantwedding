using System.Collections.Generic;

namespace BensWedding.Models
{
    public class RsvpViewModel
    {
        public int Id { get; set; }

        public int SelectedAttendingId { get; set; }

        public List<Attending> Attendings { get; set; }

        public bool IsCamping { get; set; }

        public string DietaryRequirements { get; set; }

        public string SongRequest { get; set; }

        public string Name { get; set; }

        public bool ShowMenuOptions { get; set; }

        public int? SelectedMenuOptionId { get; set; }

        public List<MenuOption> MenuOptions { get; set; }

        public List<RsvpDisplayViewModel> Rsvps { get; set; }
    }
}