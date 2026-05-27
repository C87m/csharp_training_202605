using src.Applications.Adapters;
using src.Applications.Domains;
using src.Presentations.ViewModels;
namespace src.Presentations.Adapters;
/// <summary>
/// ドメインオブジェクト:Employeeを
/// EmployeeUpdateViewModel(部署一覧ViewModel)に変換するアダプターインターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Employee</typeparam>
/// <typeparam name="TTarget">EmployeeUpdate</typeparam>
public class EmployeeUpdateViewModelAdapter : IConverter<List<Employee>, List<EmployeeUpdateViewModel>>
{
    /// <summary>
    /// EmployeeUpdateViewModelをドメインオブジェクト:Employeeに変換する
    /// </summary>
    /// <param name="target">EmployeeUpdateViewModel</param>
    /// <returns>ドメインオブジェクト:Employee</returns>
    public Employee Restore(EmployeeUpdateViewModel target)
    {
        // Department(部署)を作成する
        var department = new Department(target.DeptId!.Value,target.DeptName);
        // 登録するEmployee(従業員)を作成する
        var employee = new Employee(target.Id,target.Name!, department, target.Birthday, target.Gender??2, target.PhoneNumber!, target.Email!, target.Address!);
        return employee;
    }

    /// <summary>
    /// ドメインオブジェクト:EmployeeをEmployeeUpdateViewModelに変換する
    /// </summary>
    /// <param name="target">EmployeeUpdateViewModel</param>
    /// <returns>ドメインオブジェクト:Employee</returns>
    public List<EmployeeUpdateViewModel> Convert(List<Employee> domain)
    {
        List<EmployeeUpdateViewModel> Employees = [];
        // 表示するEmployee(部署)を作成する
        foreach(Employee emp in domain)
        {
            var Employee = new EmployeeUpdateViewModel(emp);
            Employees.Add(Employee);
        }
        return Employees;
    }
}
    