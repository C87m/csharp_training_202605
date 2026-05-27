using src.Applications.Repositories;
using src.Applications.Domains;
using src.Exceptions;
using src.Infrastructures.Context;
namespace src.Applications.Services.Impls;
/// <summary>
/// 従業員登録サービスインターフェイスの実装
/// </summary>
public class UserLoginService : IUserLoginService
{

    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインオブジェクト:部署のCRUD操作インターフェイス
    /// </summary>
    private readonly ILoginRepository _loginRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context">アプリケーション用DbContext</param>
    /// <param name="loginRepository">部署のCRUD操作インターフェイス</param>
    public UserLoginService(
        AppDbContext context,
        ILoginRepository loginRepository
        )
    {
        _context = context;
        _loginRepository = loginRepository;
    }

    /// <summary>
    /// 指定したIDの部署を取得する
    /// </summary>
    /// <returns></returns>
    public Login? GetPasswordById(string id)
    {
        var login = _loginRepository.FindById(id);
        if (login == null)
        {
            return null;
        }
        return login;
    }

}