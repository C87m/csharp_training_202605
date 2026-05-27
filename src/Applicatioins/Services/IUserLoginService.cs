using src.Applications.Domains;
namespace src.Applications.Services;
/// <summary>
/// 従業員削除サービスインターフェイス
/// </summary>
public interface IUserLoginService 
{
    /// <summary>
    /// 指定したIDの部署を取得する
    /// </summary>
    /// <returns></returns>
    Login? GetPasswordById(string id);
    
}