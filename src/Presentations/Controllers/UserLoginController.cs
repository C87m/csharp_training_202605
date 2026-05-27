using Microsoft.AspNetCore.Mvc;
using src.Applications.Services;
using src.Presentations.ViewModels;
using src.Presentations.Adapters;
using src.Applications.Domains;
using src.Controllers;
using System.Diagnostics;
namespace src.Presentations.Controllers;

[Route("UserLogin")]
public class UserLoginController : Controller
{
    public UserLoginController(){}

    /// <summary>
    /// ログイン画面表示メソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// ログイン画面表示メソッド
    /// </summary>
    /// <returns></returns>
    [HttpPost("Login")]
    public IActionResult Login(UserLoginViewModel viewModel)
    {
        if (viewModel.Id == "user" && viewModel.Password == "password")
        {
            Debug.WriteLine("do something without await...");
            return RedirectToAction("Enter", "Management");
        }
        return View();
    }
}