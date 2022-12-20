using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebTest.Models;

namespace WebTest.Controllers;

    public class HomeController : Controller {

    private readonly ILogger<HomeController> logger;

    public HomeController(ILogger<HomeController> logger) {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IActionResult Index() {
        return View();
    }

    public IActionResult Privacy() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        logger.LogError("Error caught.");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
