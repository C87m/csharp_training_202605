using src.Applications.Adapters;
using src.Applications.Domains;
using src.Presentations.ViewModels;
namespace src.Presentations.Adapters;
/// <summary>
/// ドメインオブジェクト:Employeeを
/// EmployeeShowViewModel(部署一覧ViewModel)に変換するアダプターインターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Employee</typeparam>
/// <typeparam name="TTarget">EmployeeShow</typeparam>
public class EmployeeShowViewModelAdapter : IConverter<List<Employee>, List<EmployeeShowViewModel>>
{
    /// <summary>
    /// ドメインオブジェクト:EmployeeをEmployeeRegisterViewModelに変換する
    /// </summary>
    /// <param name="target">EmployeeRegisterViewModel</param>
    /// <returns>ドメインオブジェクト:Employee</returns>
    public List<EmployeeShowViewModel> Convert(List<Employee> domain)
    {
        List<EmployeeShowViewModel> Employees = [];
        // 表示するEmployee(部署)を作成する
        foreach(Employee emp in domain)
        {
            var Employee = new EmployeeShowViewModel(emp);
            Employees.Add(Employee);
        }
        return Employees;
    }
}
    