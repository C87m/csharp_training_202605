using src.Applications.Domains;   
namespace src.Applications.Repositories;
/// <summary>
/// ドメインオブジェクト:従業員のCRUD操作インターフェイス
/// </summary>
public interface IEmployeeRepository
{
    /// <summary>
    /// すべての社員を取得する
    /// </summary>
    /// <returns>社員のリスト</returns>
    List<Employee> FindAll();
    /// <summary>
    /// すべての社員を取得する
    /// </summary>
    /// <returns>社員のリスト</returns>
    List<int?> FindAllDept();
    
    /// <summary>
    /// 従業員を永続化する
    /// </summary>
    /// <param name="employee">永続化対象の従業員</param>
    void Create(Employee employee);
}