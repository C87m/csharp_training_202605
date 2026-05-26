using src.Applications.Domains;
namespace src.Applications.Services;
/// <summary>
/// 従業員削除サービスインターフェイス
/// </summary>
public interface IEmployeeDeleteService 
{
    /// <summary>
    /// すべての部署を取得する
    /// </summary>
    /// <returns></returns>
    List<Department> GetDepartments();
    /// <summary>
    /// 指定したIDの部署を取得する
    /// </summary>
    /// <returns></returns>
    Department GetDepartmentById(int id);
    /// <summary>
    /// すべての部署を取得する
    /// </summary>
    /// <returns></returns>
    List<Employee> GetEmployees();
    /// <summary>
    /// 指定したIDの部署を取得する
    /// </summary>
    /// <returns></returns>
    Employee GetEmployeeById(int id);
    /// <summary>
    /// 部署を削除する
    /// </summary>
    /// <param name="employee"></param>
    void EmployeeDelete(Employee employee);
}