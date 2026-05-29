using Microsoft.AspNetCore.Mvc;
using src.Applications.Services;
using src.Presentations.ViewModels;
using src.Presentations.Adapters;
using src.Applications.Domains;
using src.Controllers;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
namespace src.Presentations.Controllers;

[Route("UserLogin")]
public class UserLoginController : Controller
{
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IUserLoginService _userLoginService;
    /// <summary>
    /// 従業員登録ViewModelをEmployeeに変換するアダプター
    /// </summary>
    private readonly UserLoginViewModelAdapter _adapter;
    public UserLoginController(IUserLoginService userLoginService,
        UserLoginViewModelAdapter userLoginViewModelAdapter)
    {
        _userLoginService = userLoginService;
        _adapter = userLoginViewModelAdapter;
    }

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
    public async Task<IActionResult> Login(UserLoginViewModel viewModel)
    {
        Login? user = _userLoginService.GetPasswordById(viewModel.Id!);
        if (user == null)
        {
            ModelState.AddModelError("Id", "IDかパスワードが間違っています。");
            return View();
        }
        var passwordHasher = new PasswordHasher<string>();
        var result = passwordHasher.VerifyHashedPassword(viewModel.Id!, user!.Password!, viewModel.Password!);
        if (result == PasswordVerificationResult.Success)
        {
            // **2. ユーザーの身元が確認された後、そのユーザーに関するクレームを定義**
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, viewModel.Id!)
            };

            // **3. 定義されたクレームから ClaimsIdentity を構築**
            //    ユーザーがどのように認証されたか（認証スキーム）を示す
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // **4. ClaimsIdentity から ClaimsPrincipal オブジェクトを構築**
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // **5. HttpContext.SignInAsync() を呼び出して認証Cookieを発行し、ユーザーをログインさせる**
            //    構築した ClaimsPrincipal を渡し、認証スキーム（クッキー認証）を指定する
            await HttpContext.SignInAsync(
                "CookieAuthentication",
                claimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = false, // true にするとセッションを超えてログイン状態が保持される
                });

            return RedirectToAction("Enter", "Management");
        }
        ModelState.AddModelError("Id", "IDかパスワードが間違っています。");
        return View();
    }

    /// <summary>
    /// ログアウト画面表示メソッド
    /// </summary>
    /// <returns></returns>
    //[HttpPost("Logout")]
    public async Task<IActionResult> Logout(UserLoginViewModel viewModel)
    {
        await HttpContext.SignOutAsync("CookieAuthentication");
        Console.WriteLine("ログアウト成功");
        return RedirectToAction("Index", "Home");
    }
}