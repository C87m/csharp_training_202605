using Microsoft.AspNetCore.Mvc;
using src.Applications.Services;
using src.Presentations.ViewModels;
using src.Presentations.Adapters;
using src.Applications.Domains;
namespace src.Presentations.Controllers;
/// <summary>
/// 従業員更新コントローラ
/// </summary>
[Route("DepartmentUpdate")]
public class DepartmentUpdateController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<DepartmentUpdateController> _logger;
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IDepartmentUpdateService _departmentUpdateService;
    /// <summary>
    /// 従業員登録ViewModelをDepartmentに変換するアダプター
    /// </summary>
    private readonly DepartmentUpdateViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly  TempDataStore<DepartmentUpdateViewModel> _deptDataStore;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="departmentUpdateService">従業員登録サービスインターフェイス</param>
    /// <param name="departmentUpdateViewModelAdapter">従業員登録ViewModelをDepartmentに変換するアダプター</param>
    /// <param name="empDataStore">TempDataを通じて一時的にViewModelを保存・復元するためのクラス</param>
    public DepartmentUpdateController(
        ILogger<DepartmentUpdateController> logger,
        IDepartmentUpdateService departmentUpdateService,
        DepartmentUpdateViewModelAdapter departmentUpdateViewModelAdapter,
        TempDataStore<DepartmentUpdateViewModel> deptDataStore)
    {
        _logger = logger;
        _departmentUpdateService = departmentUpdateService;
        _adapter = departmentUpdateViewModelAdapter;
        _deptDataStore = deptDataStore;
    }

    /// <summary>
    /// 部署選択画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Enter")]
    public IActionResult Enter()
    {
        DepartmentUpdateViewModel viewModel = new();
        // 部署一覧を取得してViewModelに設定する(SelectListItem形式)
        PopulateDepartments(viewModel);
        return View(viewModel);
    }

    /// <summary>
    /// 部署更新画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Edit")]
    public IActionResult Edit(int id)
    {   
        Department before_department_domain = _departmentUpdateService.GetDepartmentById(id);
        DepartmentUpdateViewModel? viewModel = null;
        // [戻る]ボタンへの対応
        // TempDataからDepartmentUpdateViewModelを取得する
        viewModel = _deptDataStore.Load(this);
        if (viewModel   == null)
        {
            // 従業員登録ViewModelを生成する
            viewModel = new DepartmentUpdateViewModel(before_department_domain);
        }
        viewModel.BeforeName = viewModel.Name;
        // viewModelをviewに渡して画面表示する
        return View(viewModel);
    }

    /// <summary>
    /// 入力画面の[完了]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost("Confirm")]
    public IActionResult Confirm(DepartmentUpdateViewModel viewModel)
    {
        // バリデーションチェック
        if (!ModelState.IsValid) // バリデーションエラーあり
        {
            // 入力画面の表示
            return View("Edit", viewModel);
        }
        // 確認画面を表示する
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[登録]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("Update")]
    public IActionResult Update(DepartmentUpdateViewModel viewModel)
    {
        // DepartmentUpdateViewModelをシリアライズして、TempDataに保存する
        _deptDataStore.Save(this, viewModel);
        // 登録処理GETアクションメソッドにリダイレクトする
        return RedirectToAction("Complete");
    }

    /// <summary>
    /// アクションメソッド:Update()のリダイレクト先
    /// PRGパターン
    /// </summary>
    /// <returns></returns>
    [HttpGet("Complete")]
    public IActionResult Complete()
    {
        DepartmentUpdateViewModel? viewModel = null;
        // TempDataからDepartmentUpdateViewModelを取得する
        viewModel = _deptDataStore.Load(this);
        if (viewModel == null)
        {
            // データが存在しない場合、入力画面にリダイレクト
            return RedirectToAction("Enter");
        }
        // DepartmentUpdateFormをドメインモデル:Departmentに変換する
        var department = _adapter.Restore(viewModel!);
        // 新しい従業員を登録する
        _departmentUpdateService.DepartmentUpdate(department);
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(DepartmentUpdateViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // DepartmentUpdateViewModelをシリアライズして、TempDataに保存する
        _deptDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Edit", new{id = viewModel.Id});
    }
    /// <summary>
    /// 部署一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateDepartments(DepartmentUpdateViewModel viewModel)
    {
        // 従業員登録サービスから部署一覧を取得する
        var departments = _departmentUpdateService.GetDepartments();
        // 部署一覧をEmployeeRegisterViewModelに登録する
        viewModel.SetDepartments(departments);
        _logger.LogInformation("部署リストを設定");
    }
}