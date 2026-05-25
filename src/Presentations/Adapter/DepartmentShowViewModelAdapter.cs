using src.Applications.Adapters;
using src.Applications.Domains;
using src.Presentations.ViewModels;
namespace src.Presentations.Adapters;
/// <summary>
/// ドメインオブジェクト:Departmentを
/// DepartmentShowViewModel(部署一覧ViewModel)に変換するアダプターインターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Department</typeparam>
/// <typeparam name="TTarget">DepartmentShow</typeparam>
public class DepartmentShowViewModelAdapter : IConverter<List<Department>, List<DepartmentShowViewModel>>
{
    /// <summary>
    /// ドメインオブジェクト:DepartmentをDepartmentRegisterViewModelに変換する
    /// </summary>
    /// <param name="target">DepartmentRegisterViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public List<DepartmentShowViewModel> Convert(List<Department> domain)
    {
        List<DepartmentShowViewModel> departments = [];
        // 表示するDepartment(部署)を作成する
        foreach(Department dept in domain)
        {
            var department = new DepartmentShowViewModel(dept);
            departments.Add(department);
        }
        return departments;
    }
}
    