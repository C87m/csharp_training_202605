using Microsoft.AspNetCore.Mvc;
using src.Applications.Services;
using src.Presentations.ViewModels;
using src.Presentations.Adapters;
using src.Applications.Domains;
namespace src.Presentations.Controllers;
/// <summary>
/// 従業員登録コントローラ
/// </summary>
[Route("EmployeeShow")]
public class EmployeeShowController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<EmployeeShowController> _logger;
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IEmployeeShowService _EmployeeShowService;
    /// <summary>
    /// 従業員登録ViewModelをEmployeeに変換するアダプター
    /// </summary>
    private readonly EmployeeShowViewModelAdapter _adapter;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="EmployeeShowService">従業員登録サービスインターフェイス</param>
    /// <param name="EmployeeShowViewModelAdapter">従業員登録ViewModelをEmployeeに変換するアダプター</param>
    public EmployeeShowController(
        ILogger<EmployeeShowController> logger,
        IEmployeeShowService EmployeeShowService,
        EmployeeShowViewModelAdapter EmployeeShowViewModelAdapter)
    {
        _logger = logger;
        _EmployeeShowService = EmployeeShowService;
        _adapter = EmployeeShowViewModelAdapter;
    }

    /// <summary>
    /// 部署登録(入力)画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Show")]
    public IActionResult Show()
    {
        List<Employee> domain = _EmployeeShowService.GetEmployees();
        List<EmployeeShowViewModel> viewModel = _adapter.Convert(domain);
        return View(viewModel);
    }
}