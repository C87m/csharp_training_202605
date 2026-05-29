using src.Applications.Domains;
namespace src.Applications.Repositories;
/// <summary>
/// ドメインオブジェクト:部署のCRUD操作インターフェイス
/// </summary>
public interface ILoginRepository
{
    /// <summary>
    /// 指定された部署Idの部署を取得する
    /// </summary>
    /// <param name="id">部署Id</param>
    /// <returns>取得して部署</returns>
    Login? FindById(string id);

    /// <summary>
    /// ユーザーを永続化する
    /// </summary>
    /// <param name="employee">永続化対象の従業員</param>
    void Create(Login user);
}