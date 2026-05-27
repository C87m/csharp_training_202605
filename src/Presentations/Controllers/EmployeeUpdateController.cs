using Microsoft.AspNetCore.Mvc;
using src.Applications.Services;
using src.Presentations.ViewModels;
using src.Presentations.Adapters;
using src.Applications.Domains;
namespace src.Presentations.Controllers;
/// <summary>
/// 従業員更新コントローラ
/// </summary>
[Route("EmployeeUpdate")]
public class EmployeeUpdateController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<EmployeeUpdateController> _logger;
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IEmployeeUpdateService _employeeUpdateService;
    /// <summary>
    /// 従業員登録ViewModelをEmployeeに変換するアダプター
    /// </summary>
    private readonly EmployeeUpdateViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly  TempDataStore<EmployeeUpdateViewModel> _empDataStore;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="employeeUpdateService">従業員登録サービスインターフェイス</param>
    /// <param name="employeeUpdateViewModelAdapter">従業員登録ViewModelをEmployeeに変換するアダプター</param>
    /// <param name="empDataStore">TempDataを通じて一時的にViewModelを保存・復元するためのクラス</param>
    public EmployeeUpdateController(
        ILogger<EmployeeUpdateController> logger,
        IEmployeeUpdateService employeeUpdateService,
        EmployeeUpdateViewModelAdapter employeeUpdateViewModelAdapter,
        TempDataStore<EmployeeUpdateViewModel> empDataStore)
    {
        _logger = logger;
        _employeeUpdateService = employeeUpdateService;
        _adapter = employeeUpdateViewModelAdapter;
        _empDataStore = empDataStore;
    }

    /// <summary>
    /// 部署選択画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Enter")]
    public IActionResult Enter()
    {
        EmployeeUpdateViewModel viewModel = new();
        // 部署一覧を取得してViewModelに設定する(SelectListItem形式)
        PopulateEmployees(viewModel);
        return View(viewModel);
    }

    /// <summary>
    /// 部署更新画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Edit")]
    public IActionResult Edit(int id)
    {   
        Employee before_employee_domain = _employeeUpdateService.GetEmployeeById(id);
        EmployeeUpdateViewModel? viewModel = null;
        // [戻る]ボタンへの対応
        // TempDataからEmployeeUpdateViewModelを取得する
        viewModel = _empDataStore.Load(this);
        if (viewModel   == null)
        {
            // 従業員登録ViewModelを生成する
            viewModel = new EmployeeUpdateViewModel(before_employee_domain);
            viewModel.BeforeName = viewModel.Name;
            viewModel.BeforeDeptId = viewModel.DeptId;
            viewModel.BeforeDeptName = viewModel.DeptName;
            viewModel.BeforeBirthday = viewModel.Birthday;
            viewModel.BeforeGender = viewModel.Gender;
            viewModel.BeforePhoneNumber = viewModel.PhoneNumber;
            viewModel.BeforeEmail = viewModel.Email;
            viewModel.BeforeAddress = viewModel.Address;
        }
        PopulateDepartments(viewModel);
        // viewModelをviewに渡して画面表示する
        return View(viewModel);
    }

    /// <summary>
    /// 入力画面の[完了]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost("Confirm")]
    public IActionResult Confirm(EmployeeUpdateViewModel viewModel)
    {
        viewModel.DeptName = _employeeUpdateService.GetDepartmentById((int)viewModel.DeptId!).Name;
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
    public IActionResult Update(EmployeeUpdateViewModel viewModel)
    {
        // EmployeeUpdateViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
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
        EmployeeUpdateViewModel? viewModel = null;
        // TempDataからEmployeeUpdateViewModelを取得する
        viewModel = _empDataStore.Load(this);
        if (viewModel == null)
        {
            // データが存在しない場合、入力画面にリダイレクト
            return RedirectToAction("Enter");
        }
        // EmployeeUpdateFormをドメインモデル:Employeeに変換する
        var employee = _adapter.Restore(viewModel!);
        // 新しい従業員を登録する
        _employeeUpdateService.EmployeeUpdate(employee);
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(EmployeeUpdateViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // EmployeeUpdateViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Edit", new{id = viewModel.Id});
    }
    /// <summary>
    /// 部署一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateEmployees(EmployeeUpdateViewModel viewModel)
    {
        // 従業員登録サービスから部署一覧を取得する
        var employees = _employeeUpdateService.GetEmployees();
        // 部署一覧をEmployeeRegisterViewModelに登録する
        viewModel.SetEmployees(employees);
        _logger.LogInformation("部署リストを設定");
    }

    /// <summary>
    /// 部署一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateDepartments(EmployeeUpdateViewModel viewModel)
    {
        // 従業員登録サービスから部署一覧を取得する
        var departments = _employeeUpdateService.GetDepartments();
        // 部署一覧をEmployeeRegisterViewModelに登録する
        viewModel.SetDepartments(departments);
        _logger.LogInformation("部署リストを設定");
    }
}