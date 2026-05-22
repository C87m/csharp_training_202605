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
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string? Name { get; set; } = string.Empty;
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
    /// 生年月日
    /// </summary>
    [Display(Name = "生年月日(YYYYNNDD)")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string? Birthday { get; set; } = string.Empty;

    /// <summary>
    /// 性別
    /// </summary>
    [Display(Name = "性別")]
    public string? Gender { get; set; } = string.Empty;

    /// <summary>
    /// 電話番号
    /// </summary>
    [Display(Name = "電話番号")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string? PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// メールアドレス
    /// </summary>
    [Display(Name = "メールアドレス")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string? Email { get; set; } = string.Empty;

    /// <summary>
    /// 住所
    /// </summary>
    [Display(Name = "住所")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string? Address { get; set; } = string.Empty;

    /// <summary>
    /// 削除フラグ
    /// </summary>
    [Display(Name = "削除フラグ")]
    public bool DeleteFlag { get; set; } = false;

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

    public override string ToString()
    {
        return $"Name={Name} , DeptId={DeptId} , DeptName={DeptName} , Departments={Departments}";
    }
}