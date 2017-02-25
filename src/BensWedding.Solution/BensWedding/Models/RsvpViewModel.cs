using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BensWedding.Models
{
    public class RsvpViewModel
    {
        public int SelectedAttendingId { get; set; }
        public List<Attending> Attendings { get; set; }

        public bool IsCamping { get; set; }

        public int SelectedMenuOptionId { get; set; }

        public List<MenuOption> MenuOptions { get; set; }
    }
}