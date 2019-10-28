using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Admin.Models;

namespace Admin.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View(new ErrorModel
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
        
    }
}