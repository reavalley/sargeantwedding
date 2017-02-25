using Newtonsoft.Json;

namespace WeddingSite.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class MenuOptionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
