using Microsoft.AspNetCore.Mvc;
using src.Applications.Services;
using src.Presentations.ViewModels;
using src.Presentations.Adapters;
using src.Applications.Domains;
namespace src.Presentations.Controllers;
using Microsoft.AspNetCore.Identity;

[Route("UserLogin")]
public class UserRegisterController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<UserRegisterController> _logger;
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IUserRegisterService _userRegisterService;
    /// <summary>
    /// 従業員登録ViewModelをEmployeeに変換するアダプター
    /// </summary>
    private readonly UserRegisterViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly  TempDataStore<UserRegisterViewModel> _usrDataStore;
    public UserRegisterController(ILogger<UserRegisterController> logger,
        IUserRegisterService userRegisterService,
        UserRegisterViewModelAdapter userRegisterViewModelAdapter,
        TempDataStore<UserRegisterViewModel> usrDataStore)
    {
        _logger = logger;
        _userRegisterService = userRegisterService;
        _adapter = userRegisterViewModelAdapter;
        _usrDataStore = usrDataStore;
    }

    /// <summary>
    /// 登録画面表示メソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Enter")]
    public IActionResult Enter()
    {
        UserRegisterViewModel? viewModel = null;
        // [戻る]ボタンへの対応
        // TempDataからDepartmentRegisterViewModelを取得する
        viewModel = _usrDataStore.Load(this);
        if (viewModel   == null)
        {
            // 従業員登録ViewModelを生成する
            viewModel = new UserRegisterViewModel();
        }
        // viewModelをviewに渡して画面表示する
        return View(viewModel);
    }

    /// <summary>
    /// 入力画面の[完了]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost("Confirm")]
    public IActionResult Confirm(UserRegisterViewModel viewModel)
    {
        Login? user = _userRegisterService.GetPasswordById(viewModel.Id!);
        if (user != null)
        {
            ModelState.AddModelError("Id", "そのIDは使用できません。");
            return View("Enter", viewModel);
        }
        // バリデーションチェック
        if (!ModelState.IsValid) // バリデーションエラーあり
        {
            // 入力画面の表示
            return View("Enter", viewModel);
        }
        // 確認画面を表示する
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[登録]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("Regiter")]
    public IActionResult Register(UserRegisterViewModel viewModel)
    {
        // DepartmentRegisterViewModelをシリアライズして、TempDataに保存する
        _usrDataStore.Save(this, viewModel);
        // 登録処理GETアクションメソッドにリダイレクトする
        return RedirectToAction("Complete");
    }

    /// <summary>
    /// アクションメソッド:Regiter()のリダイレクト先
    /// PRGパターン
    /// </summary>
    /// <returns></returns>
    [HttpGet("Complete")]
    public IActionResult Complete()
    {
        UserRegisterViewModel? viewModel = null;
        // TempDataからDepartmentRegisterViewModelを取得する
        viewModel = _usrDataStore.Load(this);
        if (viewModel == null)
        {
            // データが存在しない場合、入力画面にリダイレクト
            return RedirectToAction("Enter");
        }
        // パスワードのハッシュ化
        var passwordHasher = new PasswordHasher<string>();
        string HashPassword = passwordHasher.HashPassword(viewModel.Id!, viewModel.Password!);
        viewModel.Password = HashPassword;
        // DepartmentRegisterFormをドメインモデル:Departmentに変換する
        var user = _adapter.Restore(viewModel!);
        // 新しい従業員を登録する
        _userRegisterService.Register(user);
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(UserRegisterViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // DepartmentRegisterViewModelをシリアライズして、TempDataに保存する
        _usrDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Enter");
    }
}