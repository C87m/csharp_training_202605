using src.Applications.Domains;
namespace src.Applications.Services;
/// <summary>
/// 従業員登録サービスインターフェイス
/// </summary>
public interface IDepartmentUpdateService 
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
    /// 部署を更新する
    /// </summary>
    /// <returns></returns>
    void DepartmentUpdate(Department department);
}