using src.Exceptions;
namespace src.Applications.Domains;
/// <summary>
/// 従業員を表すドメインオブジェクト
/// </summary>
public class Employee
{
    public int? Id { get; private set; } // 社員Id
    public string Name { get; private set; } = string.Empty; // 氏名
    public Department? Department { get; private set; } // 所属部署（null可）
    public string Email {get; private set; } // メールアドレス
    public string Address {get; private set; } // 住所

    private const int NameMaxLength = 20;
    private const int AddressMaxLength = 100;
    private const int EmailMaxLength = 100;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="id">社員Id</param>
    /// <param name="name">氏名</param>
    /// <param name="department">所属部署</param>
    /// <param name="address">住所</param>
    /// <param name="email">メールアドレス</param>
    public Employee(int? id, string name, Department? department, string address, string email)
    {
        ValidateName(name);
        ValidateAddress(address);
        ValidateEmail(email);
        Id = id;
        Name = name;
        Department = department;
        Address = address;
        Email = email;
    }

    /// <summary>
    /// ID未定の社員を作成する場合のコンストラクタ
    /// </summary>
    /// <param name="name">氏名</param>
    /// <param name="department">所属部署</param>
    /// <param name="address">住所</param>
    /// <param name="email">メールアドレス</param>
    public Employee(string name, Department? department, string address, string email)
        : this(null, name, department, address, email) { }

    /// <summary>
    /// 氏名の検証
    /// </summary>
    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)||name.Length > NameMaxLength)
            throw new DomainException($"氏名は1文字以上{NameMaxLength}文字以内で入力してください");
    }

    /// <summary>
    /// 住所の検証
    /// </summary>
    private void ValidateAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address)||address.Length > AddressMaxLength)
            throw new DomainException($"住所は1文字以上{AddressMaxLength}文字以内で入力してください");
    }

    /// <summary>
    /// メールアドレスの検証
    /// </summary>
    private void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)||email.Length > EmailMaxLength)
            throw new DomainException($"メールアドレスは1文字以上{EmailMaxLength}文字以内で入力してください");
    }

    /// <summary>
    /// 氏名を変更する
    /// </summary>
    public void ChangeName(string name)
    {
        ValidateName(name);
        Name = name;
    }

    /// <summary>
    /// 所属部署を変更する
    /// </summary>
    public void ChangeDepartment(Department? department)
    {
        Department = department;
    }

    /// <summary>
    /// メールアドレスを変更する
    /// </summary>
    public void ChangeEmail(string email)
    {
        ValidateName(email);
        Email = email;
    }

    /// <summary>
    /// 住所を変更する
    /// </summary>
    public void ChangeAddress(string address)
    {
        ValidateName(address);
        Address = address;
    }

    /// <summary>
    /// 等価性（IDによる比較）
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not Employee other) return false;
        return Id == other.Id;
    }

    public override int GetHashCode() => Id?.GetHashCode() ?? 0;

    public override string ToString()
        => $"{Id?.ToString() ?? "未登録"}: {Name} / {Department?.Name ?? "未配属" } / {Address} / {Email}";
}