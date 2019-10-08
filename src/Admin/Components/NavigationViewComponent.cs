using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Admin.Models;

namespace Admin.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(MenuViewModel menu)
        {
            return View("Default", menu);
        }
    }
}
