using Admin.Factories;

namespace Admin.Models
{
    public class BaseModel
    {
        public UserModel User { get; set; }
        public MenuModel Menu { get; set; }
        public UrlFactory UrlFactory { get; set; }
    }
}
