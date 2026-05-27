using src.Applications.Adapters;
using src.Applications.Domains;
using src.Infrastructures.Entities;
namespace src.Infrastructures.Adapters;
/// <summary>
/// ドメインオブジェクト:LoginとLoginEntityの相互変換インターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Login</typeparam>
/// <typeparam name="TTarget">LoginEntity</typeparam>
public class LoginEntityAdapter :
IConverter<Login, LoginEntity>,IRestorer<Login, LoginEntity>
{
    // <summary>
    /// ドメインオブジェクト:LoginをLoginEntityに変換する
    /// </summary>
    /// <param name="domain">ドメインオブジェクト:Login</param>
    /// <returns>LoginEntity</returns>
    public LoginEntity Convert(Login domain)
    {
        var entity = new LoginEntity{
            Password = domain.Password!,
            LoginId = domain.Id!
        };
        return entity;
    }

    /// <summary>
    /// LoginEntityからドメインオブジェクト:Loginを復元する
    /// </summary>
    /// <param name="entity">LoginEntity</param>
    /// <returns>ドメインオブジェクト:Login</returns>
    public Login Restore(LoginEntity target)
    {
        var department = new Login(target.LoginId,target.Password);
        return department;
    }
}