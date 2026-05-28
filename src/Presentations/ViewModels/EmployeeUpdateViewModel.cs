using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Applications.Domains;
namespace src.Presentations.ViewModels;
/// <summary>
/// 部署登録ViewModelクラス
/// </summary>
public class EmployeeUpdateViewModel
{
    /// <summary>
    /// 氏名
    /// </summary>
    [Display(Name = "ID")]
    public int? Id { get; set; } = 0;
    /// <summary>
    /// 氏名
    /// </summary>
    [Display(Name = "氏名")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [StringLength(10, ErrorMessage = "{0}は1文字以上10文字以内で入力してください。")]
    public string? Name { get; set; } = string.Empty;
    /// <summary>
    /// 氏名
    /// </summary>
    [Display(Name = "氏名")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [StringLength(10, ErrorMessage = "{0}は1文字以上10文字以内で入力してください。")]
    public string? BeforeName { get; set; } = string.Empty;
    /// <summary>
    /// 生年月日
    /// </summary>
    [Display(Name = "生年月日")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [Range(typeof(DateOnly), "1/1/1900", "01/01/2100", ErrorMessage = "生年月日が不正です。")]
    public DateOnly Birthday { get; set; }
    /// <summary>
    /// 生年月日
    /// </summary>
    [Display(Name = "生年月日")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [Range(typeof(DateOnly), "1900/01/01", "2026/05/29", ErrorMessage = "未来の日付は設定できません。")]
    public DateOnly BeforeBirthday { get; set; }
    /// <summary>
    /// 所属部署
    /// </summary>
    [Display(Name = "所属部署")]
    [Required(ErrorMessage = "{0}は選択必須です。")]
    public int? DeptId { get; set; } = 0;
    /// <summary>
    /// 所属部署
    /// </summary>
    [Display(Name = "所属部署")]
    [Required(ErrorMessage = "{0}は選択必須です。")]
    public int? BeforeDeptId { get; set; } = 0;

    /// <summary>
    /// 選択された部署名
    /// </summary>
    [Display(Name = "部署名")]
    public string? DeptName { get; set; } = string.Empty;
    /// <summary>
    /// 選択された部署名
    /// </summary>
    [Display(Name = "部署名")]
    public string? BeforeDeptName { get; set; } = string.Empty;

    /// <summary>
    /// 性別
    /// </summary>
    [Display(Name = "性別")]
    public int? Gender { get; set; } = 2;
    /// <summary>
    /// 性別
    /// </summary>
    [Display(Name = "性別")]
    public int? BeforeGender { get; set; } = 2;

    /// <summary>
    /// 電話番号
    /// </summary>
    [Display(Name = "電話番号")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [Phone(ErrorMessage = "電話番号の形式が正しくありません")]
    public string? PhoneNumber { get; set; } = string.Empty;
    /// <summary>
    /// 電話番号
    /// </summary>
    [Display(Name = "電話番号")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [Phone(ErrorMessage = "電話番号の形式が正しくありません")]
    public string? BeforePhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// メールアドレス
    /// </summary>
    [Display(Name = "メールアドレス")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [EmailAddress(ErrorMessage = "メールアドレスの形式が正しくありません")]
    public string? Email { get; set; } = string.Empty;
    /// <summary>
    /// メールアドレス
    /// </summary>
    [Display(Name = "メールアドレス")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [EmailAddress(ErrorMessage = "メールアドレスの形式が正しくありません")]
    public string? BeforeEmail { get; set; } = string.Empty;

    /// <summary>
    /// 住所
    /// </summary>
    [Display(Name = "住所")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string? Address { get; set; } = string.Empty;
    /// <summary>
    /// 住所
    /// </summary>
    [Display(Name = "住所")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string? BeforeAddress { get; set; } = string.Empty;

    /// <summary>
    /// 部署のリストをSelectListItemのリストに変換してプロパティに設定する
    /// </summary>
    /// <param name="departments"></param>
    public void SetDepartments(List<Department> departments)
    {
        // SelectListItemのリストを作成
        var selectItems = new List<SelectListItem>();
        foreach (var dept in departments)
        {
            if (dept.Id.HasValue)
            {
                var item = new SelectListItem();
                item.Value = dept.Id.Value.ToString();
                item.Text = string.IsNullOrEmpty(dept.Name) ? "(名称未設定)" : dept.Name;
                selectItems.Add(item);
            }
        }
        Departments = selectItems;
    }
    // 部署のリスト
    public List<SelectListItem>? Departments { get; set; } = null;

    /// <summary>
    /// 性別をプルダウン表示するリスト
    /// </summary>
    /// <value></value>
    public List<SelectListItem> GenderList { get; set; } = new List<SelectListItem>
    {
        new SelectListItem{ Text="男", Value="0" , Selected = true },
        new SelectListItem{ Text= "女", Value= "1" },
        new SelectListItem{ Text= "その他", Value= "2" },
    };

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public EmployeeUpdateViewModel(Employee domain)
    {
        Name = domain.Name;
        DeptId = domain.Department?.Id ?? 1;
        DeptName = domain.Department?.Name ?? "無所属";
        Birthday = domain.Birthday;
        Gender = domain.Gender;
        PhoneNumber = domain.PhoneNumber;
        Email = domain.Email;
        Address = domain.Address;
    }

    public EmployeeUpdateViewModel(){}

    /// <summary>
    /// 部署のリストをSelectListItemのリストに変換してプロパティに設定する
    /// </summary>
    /// <param name="departments"></param>
    public void SetEmployees(List<Employee> employees)
    {
        // SelectListItemのリストを作成
        var selectItems = new List<SelectListItem>();
        foreach (var emp in employees)
        {
            if (emp.Id.HasValue)
            {
                var item = new SelectListItem();
                item.Value = emp.Id.Value.ToString();
                item.Text = string.IsNullOrEmpty(emp.Name) ? "(名称未設定)" : emp.Name;
                selectItems.Add(item);
            }
        }
        Employees = selectItems;
    }
    // 部署のリスト
    public List<SelectListItem>? Employees { get; set; } = null;

    public override string ToString()
    {
        return $"Name={Name} , DeptId={DeptId} , DeptName={DeptName} , Departments={Departments} , Birthday={Birthday}, PhoneNumber={PhoneNumber} , Email={Email} , Address={Address}";
    }
}