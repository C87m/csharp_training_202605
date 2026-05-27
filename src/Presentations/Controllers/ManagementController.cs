using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using src.Presentations.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace src.Controllers;
[Authorize]
public class ManagementController : Controller
{
    private readonly ILogger<ManagementController> _logger;

    public ManagementController(ILogger<ManagementController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    public IActionResult Enter()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
