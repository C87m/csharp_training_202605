using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Applications.Domains;
namespace src.Presentations.ViewModels;
/// <summary>
/// 部署更新ViewModelクラス
/// </summary>
///
/// コンストラクタ
/// 
public class DepartmentDeleteViewModel
{
    /// <summary>
    /// 選択された部署名
    /// </summary>
    [Display(Name = "部署ID")]
    public int? Id { get; set; }

    /// <summary>
    /// 選択された部署名
    /// </summary>
    [Display(Name = "部署名")]
    public string? Name { get; set; }

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
            if (dept.Id.HasValue && dept.Id != 1)
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

    /// 
    /// コンストラクタ
    /// 
    public DepartmentDeleteViewModel(Department domain)
    {
        Id = domain.Id;
        Name = domain.Name;
    }

    public DepartmentDeleteViewModel(){}

    public override string ToString()
    {
        return $"DeptId={Id}, DeptName={Name}";
    }
}