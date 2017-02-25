using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace WeddingSite.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class RsvpViewModel
    {
        public IEnumerable<string> Attending { get; set; }

        [DefaultValue(false)]
        public bool IsCamping { get; set; }
        
        public IEnumerable<MenuOptionViewModel> MenuOptions { get; set; }
    }
}
