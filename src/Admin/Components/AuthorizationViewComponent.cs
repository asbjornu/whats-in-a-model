using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Admin.Models;

namespace Admin.Components
{
    public class AuthorizationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(UserViewModel user)
        {
            return View("Default", user);
        }
    }
}
