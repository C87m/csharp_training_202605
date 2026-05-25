using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Applications.Domains;
namespace src.Presentations.ViewModels;
/// <summary>
/// 部署登録ViewModelクラス
/// </summary>
public class DepartmentRegisterViewModel
{
    /// <summary>
    /// 選択された部署名
    /// </summary>
    [Display(Name = "部署名")]
    [Required(ErrorMessage = "{0}は1文字以上10文字以内で入力してください。")]
    [StringLength(10, ErrorMessage = "{0}は1文字以上10文字以内で入力してください。")]
    public string? Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"DeptName={Name}";
    }
}