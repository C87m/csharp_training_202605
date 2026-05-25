using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Applications.Domains;
namespace src.Presentations.ViewModels;
/// <summary>
/// 部署一覧ViewModelクラス
/// </summary>
///
/// コンストラクタ
/// 
public class DepartmentShowViewModel(Department domain)
{
    /// <summary>
    /// 選択された部署名
    /// </summary>
    [Display(Name = "部署名")]
    public string? Name { get; set; } = domain.Name;

    public override string ToString()
    {
        return $"DeptName={Name}";
    }
}