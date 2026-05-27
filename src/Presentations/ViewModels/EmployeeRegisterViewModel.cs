using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Applications.Domains;
namespace src.Presentations.ViewModels;
/// <summary>
/// 部署登録ViewModelクラス
/// </summary>
public class EmployeeRegisterViewModel
{
    /// <summary>
    /// 氏名
    /// </summary>
    [Display(Name = "氏名")]
    [Required(ErrorMessage = "{0}は1文字以上10文字以内で入力してください。")]
    [StringLength(10, ErrorMessage = "{0}は1文字以上10文字以内で入力してください。")]
    public string? Name { get; set; } = string.Empty;
    /// <summary>
    /// 生年月日
    /// </summary>
    [Display(Name = "生年月日")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [Range(typeof(DateOnly), "1/1/1900", "5/29/2026", ErrorMessage = "未来の日付は設定できません。")]
    public DateOnly Birthday { get; set; } = DateOnly.Parse("2003/01/01");
    /// <summary>
    /// 所属部署
    /// </summary>
    [Display(Name = "所属部署")]
    [Required(ErrorMessage = "{0}は選択必須です。")]
    public int? DeptId { get; set; } = 0;

    /// <summary>
    /// 選択された部署名
    /// </summary>
    [Display(Name = "部署名")]
    public string? DeptName { get; set; } = string.Empty;

    /// <summary>
    /// 性別
    /// </summary>
    [Display(Name = "性別")]
    public int Gender { get; set; } = 2;

    /// <summary>
    /// 電話番号
    /// </summary>
    [Display(Name = "電話番号")]
    [Required(ErrorMessage = "{0}は必須です。")]
    [Phone(ErrorMessage = "電話番号が不正です")]
    [StringLength(12, ErrorMessage = "{0}が不正です")]
    public string? PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// メールアドレス
    /// </summary>
    [Display(Name = "メールアドレス")]
    [Required(ErrorMessage = "{0}は必須です。")]
    [EmailAddress(ErrorMessage = "{0}が不正です")]
    [StringLength(100, ErrorMessage = "{0}が不正です")]
    public string? Email { get; set; } = string.Empty;

    /// <summary>
    /// 住所
    /// </summary>
    [Display(Name = "住所")]
    [Required(ErrorMessage = "{0}は必須です。")]
    [StringLength(100, ErrorMessage = "{0}が不正です")]
    public string? Address { get; set; } = string.Empty;

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

    public override string ToString()
    {
        return $"Name={Name} , DeptId={DeptId} , DeptName={DeptName} , Departments={Departments} , Birthday={Birthday}, PhoneNumber={PhoneNumber} , Email={Email} , Address={Address}";
    }
}