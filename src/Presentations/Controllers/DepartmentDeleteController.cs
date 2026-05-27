using Microsoft.AspNetCore.Mvc;
using src.Applications.Services;
using src.Presentations.ViewModels;
using src.Presentations.Adapters;
using src.Applications.Domains;
using Microsoft.AspNetCore.Authorization;
namespace src.Presentations.Controllers;
/// <summary>
/// 従業員削除コントローラ
/// </summary>
[Route("DepartmentDelete")]
[Authorize]
public class DepartmentDeleteController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<DepartmentDeleteController> _logger;
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IDepartmentDeleteService _departmentDeleteService;
    /// <summary>
    /// 従業員登録ViewModelをDepartmentに変換するアダプター
    /// </summary>
    private readonly DepartmentDeleteViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly  TempDataStore<DepartmentDeleteViewModel> _deptDataStore;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="departmentDeleteService">従業員削除サービスインターフェイス</param>
    /// <param name="departmentDeleteViewModelAdapter">従業員削除ViewModelをDepartmentに変換するアダプター</param>
    /// <param name="empDataStore">TempDataを通じて一時的にViewModelを保存・復元するためのクラス</param>
    public DepartmentDeleteController(
        ILogger<DepartmentDeleteController> logger,
        IDepartmentDeleteService departmentDeleteService,
        DepartmentDeleteViewModelAdapter departmentDeleteViewModelAdapter,
        TempDataStore<DepartmentDeleteViewModel> deptDataStore)
    {
        _logger = logger;
        _departmentDeleteService = departmentDeleteService;
        _adapter = departmentDeleteViewModelAdapter;
        _deptDataStore = deptDataStore;
    }

    /// <summary>
    /// 部署選択画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Enter")]
    public IActionResult Enter()
    {
        DepartmentDeleteViewModel viewModel = new();
        // 部署一覧を取得してViewModelに設定する(SelectListItem形式)
        PopulateDepartments(viewModel);
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
        Department domain = _departmentDeleteService.GetDepartmentById(id);
        var viewModel = new DepartmentDeleteViewModel(domain);
        // 確認画面を表示する
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[登録]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public IActionResult Delete(DepartmentDeleteViewModel viewModel)
    {
        // DepartmentDeleteViewModelをシリアライズして、TempDataに保存する
        _deptDataStore.Save(this, viewModel);
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
        DepartmentDeleteViewModel? viewModel = null;
        // TempDataからDepartmentDeleteViewModelを取得する
        viewModel = _deptDataStore.Load(this);
        if (viewModel == null)
        {
            // データが存在しない場合、入力画面にリダイレクト
            return RedirectToAction("Enter");
        }
        // DepartmentDeleteFormをドメインモデル:Departmentに変換する
        var department = _adapter.Restore(viewModel!);
        // 新しい従業員を登録する
        _departmentDeleteService.DepartmentDelete(department);
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(DepartmentDeleteViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // DepartmentDeleteViewModelをシリアライズして、TempDataに保存する
        _deptDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Enter");
    }
    /// <summary>
    /// 部署一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateDepartments(DepartmentDeleteViewModel viewModel)
    {
        // 従業員登録サービスから部署一覧を取得する
        var departments = _departmentDeleteService.GetDepartments();
        // 部署一覧をEmployeeRegisterViewModelに登録する
        viewModel.SetDepartments(departments);
        _logger.LogInformation("部署リストを設定");
    }
}