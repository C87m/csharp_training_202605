using src.Applications.Domains;
namespace src.Applications.Services;
/// <summary>
/// 従業員削除サービスインターフェイス
/// </summary>
public interface IDepartmentDeleteService 
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
    /// 指定したIDの部署に所属する人数を返す
    /// </summary>
    /// <returns></returns>
    int CountDeptMemberById(int id);
    /// <summary>
    /// 部署を削除する
    /// </summary>
    /// <param name="department"></param>
    void DepartmentDelete(Department department);
}