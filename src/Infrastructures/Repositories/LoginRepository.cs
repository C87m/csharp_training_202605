using src.Infrastructures.Context;
using src.Applications.Domains;
using src.Applications.Repositories;
using src.Infrastructures.Adapters;
using src.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace src.Infrastructures.Repositories;
/// <summary>
/// ドメインオブジェクト:部署のCRUD操作インターフェイス実装
/// </summary>
public class LoginRepository : ILoginRepository
{
    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインモデル:部署と部署エンティティの相互変換インターフェイスの実装
    /// </summary>
    private readonly LoginEntityAdapter _adapter;
    
    public LoginRepository(AppDbContext context, LoginEntityAdapter adapter)
    {
        _context = context;
        _adapter = adapter;
    }

    /// <summary>
    /// 指定された部署Idの部署を取得する
    /// </summary>
    /// <param name="id">部署Id</param>
    /// <returns>取得して部署</returns>
    public Login? FindById(string id)
    {
        try
        {
            var result = _context.Login.FirstOrDefault(d => d.LoginId == id);
            if (result == null)
            {
                return null;
            }
            return _adapter.Restore(result);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "指定されたIdのユーザーを取得できませんでした。", e);
        }
    }

    /// <summary>
    /// ユーザーを永続化
    /// </summary>
    /// <param name="department">永続化対象の部署</param>
    public void Create(Login user)
    {
        try
        {
            var entity = _adapter.Convert(user);
            _context.Login.Add(entity);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new InternalException(
                "ユーザーの永続化ができませんでした。", e);
        }
    }

}