using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? statusCode, string message)
    {
        ViewBag.StatusCode = statusCode;
        ViewBag.Message = message;

        return View();
    }
}