using Admin.Factories;

namespace Admin.Models
{
    public class BaseViewModel
    {
        public UserViewModel User { get; set; }
        public MenuViewModel Menu { get; set; }
        public UrlFactory UrlFactory { get; set; }
    }
}
