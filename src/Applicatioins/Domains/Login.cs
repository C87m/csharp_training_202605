using src.Exceptions;
namespace src.Applications.Domains;
/// <summary>
/// 所属部署を表すドメインオブジェクト
/// </summary>
public class Login
{
    public string? Id { get; private set; }      // 部署Id
    public string? Password { get; private set; } = string.Empty;    // 部署名
    private const int MaxLength = 10; // 部署名の長さ
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="id">部署Id</param>
    /// <param name="name">部署名</param>
    public Login(string? id, string? password)
    {
        Id = id;
        Password = password;
    }
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="name">部署名</param>
    public Login(string? password) : this(null, password) { }

    

    /// <summary>
    /// 等価性の検証
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not Login other) return false;
        return Id == other.Id;
    }
    public override int GetHashCode() => Id?.GetHashCode() ?? 0;

    public override string ToString() => $"{Id?.ToString() ?? "未登録"}: {Password}";
}