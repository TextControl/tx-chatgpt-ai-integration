using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using tx_chatgpt_ai_integration.Models;

namespace tx_chatgpt_ai_integration.Controllers {

   [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
   public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}