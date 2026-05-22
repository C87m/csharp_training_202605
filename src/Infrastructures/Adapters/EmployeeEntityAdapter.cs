using src.Applications.Adapters;
using src.Applications.Domains;
using src.Infrastructures.Entities;
namespace src.Infrastructures.Adapters;
/// <summary>
/// ドメインオブジェクト:EmployeeとEmployeeEntityの相互変換インターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Employee</typeparam>
/// <typeparam name="TTarget">EmployeeEntity</typeparam>
public class EmployeeEntityAdapter :
IConverter<Employee, EmployeeEntity>, IRestorer<Employee, EmployeeEntity>
{
    /// <summary>
    /// ドメインオブジェクト:EmployeeをEmployeeEntityに変換する
    /// </summary>
    /// <param name="domain">ドメインモデル:従業員</param>
    /// <returns>EmployeeEntity</returns>
    public EmployeeEntity Convert(Employee domain)
    {
        var entity = new EmployeeEntity{
            EmpName = domain.Name
        };
        if (domain.Id != null){
            entity.EmpId = domain.Id.Value;
        }
        if (domain.Department != null)
        {
            entity.DeptId = domain.Department.Id;
        }
        if (domain.Birthday != null)
        {
            entity.Birthday = domain.Birthday;
        }
        if (domain.Gender != null)
        {
            entity.Gender = domain.Gender;
        }
        if (domain.PhoneNumber != null)
        {
            entity.PhoneNumber = domain.PhoneNumber;
        }
        if (domain.Email != null)
        {
            entity.Email = domain.Email;
        }
        if (domain.Address != null)
        {
            entity.Address = domain.Address;
        }
        return entity;
    }

    /// <summary>
    /// EmployeeEntityからドメインオブジェクト:Employeeを復元する
    /// </summary>
    /// <param name="target">EmployeeEntity</param>
    /// <returns>ドメインオブジェクト:Employee</returns>
    public Employee Restore(EmployeeEntity target)
    {
        var employee = new Employee(
            target.EmpId,
            target.EmpName,
            null,
            target.Birthday,
            target.Gender,
            target.PhoneNumber,
            target.Email,
            target.Address,
            target.DeleteFlag
        );
        return employee;
    }
}