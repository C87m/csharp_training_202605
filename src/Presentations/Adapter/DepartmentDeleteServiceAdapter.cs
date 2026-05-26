using src.Applications.Adapters;
using src.Applications.Domains;
using src.Presentations.ViewModels;
namespace src.Presentations.Adapters;
/// <summary>
/// DepartmentDeleteViewModel(部署登録ViewModel)を
/// ドメインオブジェクト:Departmentに変換するアダプターインターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Department</typeparam>
/// <typeparam name="TTarget">DepartmentDeleteForm</typeparam>
public class DepartmentDeleteViewModelAdapter : IRestorer<Department, DepartmentDeleteViewModel>
{
    /// <summary>
    /// DepartmentDeleteViewModelをドメインオブジェクト:Departmentに変換する
    /// </summary>
    /// <param name="target">DepartmentDeleteViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public Department Restore(DepartmentDeleteViewModel target)
    {
        // 登録するDepartment(部署)を作成する
        var department = new Department(target.Id, target.Name);
        return department;
    }

    /// <summary>
    /// ドメインオブジェクト:DepartmentをDepartmentRegisterViewModelに変換する
    /// </summary>
    /// <param name="target">DepartmentRegisterViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public List<DepartmentDeleteViewModel> Convert(List<Department> domain)
    {
        List<DepartmentDeleteViewModel> departments = [];
        // 表示するDepartment(部署)を作成する
        foreach(Department dept in domain)
        {
            var department = new DepartmentDeleteViewModel(dept);
            departments.Add(department);
        }
        return departments;
    }

    /// <summary>
    /// ドメインオブジェクト:DepartmentをDepartmentRegisterViewModelに変換する
    /// </summary>
    /// <param name="target">DepartmentRegisterViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public DepartmentDeleteViewModel Convert(Department domain)
    {
        DepartmentDeleteViewModel department = new(domain);
        
        return department;
    }
}