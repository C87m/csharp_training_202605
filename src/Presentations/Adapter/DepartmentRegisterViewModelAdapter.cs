using src.Applications.Adapters;
using src.Applications.Domains;
using src.Presentations.ViewModels;
namespace src.Presentations.Adapters;
/// <summary>
/// DepartmentRegisterViewModel(部署登録ViewModel)を
/// ドメインオブジェクト:Departmentに変換するアダプターインターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Department</typeparam>
/// <typeparam name="TTarget">DepartmentRegisterForm</typeparam>
public class DepartmentRegisterViewModelAdapter : IRestorer<Department, DepartmentRegisterViewModel>
{
    /// <summary>
    /// DepartmentRegisterViewModelをドメインオブジェクト:Departmentに変換する
    /// </summary>
    /// <param name="target">DepartmentRegisterViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public Department Restore(DepartmentRegisterViewModel target)
    {
        // 登録するDepartment(部署)を作成する
        var department = new Department(target.Name!);
        return department;
    }
}