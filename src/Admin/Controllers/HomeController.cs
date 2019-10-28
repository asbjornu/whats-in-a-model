using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Admin.Models;
using Admin.Services;
using Admin.Factories;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly NavigationService navigationService;
        private readonly AuthorizationService authorizationService;

        public HomeController(NavigationService navigationService, AuthorizationService authorizationService)
        {
            this.navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this.authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public async Task<IActionResult> Index()
        {
            var menu = await this.navigationService.GetMenuAsync();
            var user = await this.authorizationService.GetAuthorizedUserAsync();
            var urlFactory = new UrlFactory(Url);
            var homeModel = new HomeModel
            {
                Menu = menu,
                User = user,
                UrlFactory = urlFactory
            };

            return View(homeModel);
        }
    }
}
