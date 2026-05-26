using src.Applications.Adapters;
using src.Applications.Domains;
using src.Presentations.ViewModels;
namespace src.Presentations.Adapters;
/// <summary>
/// DepartmentUpdateViewModel(部署登録ViewModel)を
/// ドメインオブジェクト:Departmentに変換するアダプターインターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Department</typeparam>
/// <typeparam name="TTarget">DepartmentUpdateForm</typeparam>
public class DepartmentUpdateViewModelAdapter : IRestorer<Department, DepartmentUpdateViewModel>
{
    /// <summary>
    /// DepartmentUpdateViewModelをドメインオブジェクト:Departmentに変換する
    /// </summary>
    /// <param name="target">DepartmentUpdateViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public Department Restore(DepartmentUpdateViewModel target)
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
    public List<DepartmentUpdateViewModel> Convert(List<Department> domain)
    {
        List<DepartmentUpdateViewModel> departments = [];
        // 表示するDepartment(部署)を作成する
        foreach(Department dept in domain)
        {
            var department = new DepartmentUpdateViewModel(dept);
            departments.Add(department);
        }
        return departments;
    }

    /// <summary>
    /// ドメインオブジェクト:DepartmentをDepartmentRegisterViewModelに変換する
    /// </summary>
    /// <param name="target">DepartmentRegisterViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public DepartmentUpdateViewModel Convert(Department domain)
    {
        DepartmentUpdateViewModel department = new(domain);
        
        return department;
    }
}