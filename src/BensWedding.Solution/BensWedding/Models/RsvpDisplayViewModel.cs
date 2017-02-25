using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BensWedding.Models
{
    public class RsvpDisplayViewModel
    {
        public string Name { get; set; }

        public string Attending { get; set; }

        public bool IsCamping { get; set; }

        public string DietaryRequirements { get; set; }

        public string MenuOption { get; set; }
    }
}