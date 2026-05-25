using src.Applications.Domains;
namespace src.Applications.Services;
/// <summary>
/// 従業員登録サービスインターフェイス
/// </summary>
public interface IDepartmentRegisterService 
{
    /// <summary>
    /// 新しい部署を登録する
    /// </summary>
    /// <param name="department"></param>
    void DepartmentRegister(Department department);
}