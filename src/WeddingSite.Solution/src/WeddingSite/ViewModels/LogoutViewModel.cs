using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WeddingSite.ViewModels
{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}
