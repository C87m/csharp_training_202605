using Microsoft.AspNetCore.Mvc;
using src.Applications.Services;
using src.Presentations.ViewModels;
using src.Presentations.Adapters;
using src.Applications.Domains;
using Microsoft.AspNetCore.Authorization;
namespace src.Presentations.Controllers;
/// <summary>
/// 従業員登録コントローラ
/// </summary>
[Route("DepartmentShow")]
[Authorize]
public class DepartmentShowController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<DepartmentShowController> _logger;
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IDepartmentShowService _departmentShowService;
    /// <summary>
    /// 従業員登録ViewModelをDepartmentに変換するアダプター
    /// </summary>
    private readonly DepartmentShowViewModelAdapter _adapter;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="departmentShowService">従業員登録サービスインターフェイス</param>
    /// <param name="departmentShowViewModelAdapter">従業員登録ViewModelをDepartmentに変換するアダプター</param>
    public DepartmentShowController(
        ILogger<DepartmentShowController> logger,
        IDepartmentShowService departmentShowService,
        DepartmentShowViewModelAdapter departmentShowViewModelAdapter)
    {
        _logger = logger;
        _departmentShowService = departmentShowService;
        _adapter = departmentShowViewModelAdapter;
    }

    /// <summary>
    /// 部署登録(入力)画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Show")]
    public IActionResult Show()
    {
        List<Department> domain = _departmentShowService.GetDepartments();
        List<DepartmentShowViewModel> viewModel = _adapter.Convert(domain);
        return View(viewModel);
    }
}