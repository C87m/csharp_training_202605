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
    Employee FindById(int id);
    /// <summary>
    /// すべての社員を取得する
    /// </summary>
    /// <returns>社員のリスト</returns>
    List<int?> FindAllDept();
    /// <summary>
    /// Idから部署Idを取得する
    /// </summary>
    /// <returns>社員のリスト</returns>
    int FindDeptIdById(int id);

    /// <summary>
    /// 指定された部署Idの部署の人数取得する
    /// </summary>
    /// <param name="id">部署Id</param>
    /// <returns>取得して部署</returns>
    List<Employee> FindMember(int id);
    
    /// <summary>
    /// 従業員を永続化する
    /// </summary>
    /// <param name="employee">永続化対象の従業員</param>
    void Create(Employee employee);

    /// <summary>
    /// 部署を更新する
    /// </summary>
    /// <param name="department">更新対象の部署</param>
    public void Renew(Employee employee);

    /// <summary>
    /// 部署を削除する
    /// </summary>
    /// <param name="department">更新対象の部署</param>
    public void Delete(Employee employee);
}