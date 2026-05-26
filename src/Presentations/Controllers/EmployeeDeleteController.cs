using Microsoft.AspNetCore.Mvc;
using src.Applications.Services;
using src.Presentations.ViewModels;
using src.Presentations.Adapters;
using src.Applications.Domains;
namespace src.Presentations.Controllers;
/// <summary>
/// 従業員削除コントローラ
/// </summary>
[Route("EmployeeDelete")]
public class EmployeeDeleteController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<EmployeeDeleteController> _logger;
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IEmployeeDeleteService _employeeDeleteService;
    /// <summary>
    /// 従業員登録ViewModelをEmployeeに変換するアダプター
    /// </summary>
    private readonly EmployeeDeleteViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly  TempDataStore<EmployeeDeleteViewModel> _empDataStore;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="departmentDeleteService">従業員削除サービスインターフェイス</param>
    /// <param name="departmentDeleteViewModelAdapter">従業員削除ViewModelをEmployeeに変換するアダプター</param>
    /// <param name="empDataStore">TempDataを通じて一時的にViewModelを保存・復元するためのクラス</param>
    public EmployeeDeleteController(
        ILogger<EmployeeDeleteController> logger,
        IEmployeeDeleteService employeeDeleteService,
        EmployeeDeleteViewModelAdapter employeeDeleteViewModelAdapter,
        TempDataStore<EmployeeDeleteViewModel> empDataStore)
    {
        _logger = logger;
        _employeeDeleteService = employeeDeleteService;
        _adapter = employeeDeleteViewModelAdapter;
        _empDataStore = empDataStore;
    }

    /// <summary>
    /// 部署選択画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Enter")]
    public IActionResult Enter()
    {
        EmployeeDeleteViewModel viewModel = new();
        // 部署一覧を取得してViewModelに設定する(SelectListItem形式)
        PopulateEmployees(viewModel);
        return View(viewModel);
    }

    /// <summary>
    /// 入力画面の[完了]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost("Confirm")]
    public IActionResult Confirm(int id)
    {
        Employee domain = _employeeDeleteService.GetEmployeeById(id);
        var viewModel = new EmployeeDeleteViewModel(domain);
        // 確認画面を表示する
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[登録]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public IActionResult Delete(EmployeeDeleteViewModel viewModel)
    {
        // EmployeeDeleteViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 登録処理GETアクションメソッドにリダイレクトする
        return RedirectToAction("Complete");
    }

    /// <summary>
    /// アクションメソッド:Delete()のリダイレクト先
    /// PRGパターン
    /// </summary>
    /// <returns></returns>
    [HttpGet("Complete")]
    public IActionResult Complete()
    {
        EmployeeDeleteViewModel? viewModel = null;
        // TempDataからEmployeeDeleteViewModelを取得する
        viewModel = _empDataStore.Load(this);
        if (viewModel == null)
        {
            // データが存在しない場合、入力画面にリダイレクト
            return RedirectToAction("Enter");
        }
        // EmployeeDeleteFormをドメインモデル:Employeeに変換する
        var department = _adapter.Restore(viewModel!);
        // 新しい従業員を登録する
        _employeeDeleteService.EmployeeDelete(department);
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(EmployeeDeleteViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // EmployeeDeleteViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Enter");
    }
    /// <summary>
    /// 部署一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateEmployees(EmployeeDeleteViewModel viewModel)
    {
        // 従業員登録サービスから部署一覧を取得する
        var employees = _employeeDeleteService.GetEmployees();
        // 部署一覧をEmployeeRegisterViewModelに登録する
        viewModel.SetEmployees(employees);
        _logger.LogInformation("社員リストを設定");
    }
}