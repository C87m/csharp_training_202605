using src.Applications.Adapters;
using src.Applications.Domains;
using src.Presentations.ViewModels;
namespace src.Presentations.Adapters;
/// <summary>
/// ドメインオブジェクト:Employeeを
/// EmployeeDeleteViewModel(部署一覧ViewModel)に変換するアダプターインターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Employee</typeparam>
/// <typeparam name="TTarget">EmployeeDelete</typeparam>
public class EmployeeDeleteViewModelAdapter : IConverter<List<Employee>, List<EmployeeDeleteViewModel>>
{
    /// <summary>
    /// EmployeeDeleteViewModelをドメインオブジェクト:Employeeに変換する
    /// </summary>
    /// <param name="target">EmployeeDeleteViewModel</param>
    /// <returns>ドメインオブジェクト:Employee</returns>
    public Employee Restore(EmployeeDeleteViewModel target)
    {
        // Department(部署)を作成する
        var department = new Department(target.DeptId!.Value,target.DeptName);
        // 登録するEmployee(従業員)を作成する
        var employee = new Employee(target.Id,target.Name!, department, target.Birthday, target.Gender??2, target.PhoneNumber!, target.Email!, target.Address!);
        return employee;
    }

    /// <summary>
    /// ドメインオブジェクト:EmployeeをEmployeeDeleteViewModelに変換する
    /// </summary>
    /// <param name="target">EmployeeDeleteViewModel</param>
    /// <returns>ドメインオブジェクト:Employee</returns>
    public List<EmployeeDeleteViewModel> Convert(List<Employee> domain)
    {
        List<EmployeeDeleteViewModel> Employees = [];
        // 表示するEmployee(部署)を作成する
        foreach(Employee emp in domain)
        {
            var Employee = new EmployeeDeleteViewModel(emp);
            Employees.Add(Employee);
        }
        return Employees;
    }
}
    