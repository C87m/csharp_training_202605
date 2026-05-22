using src.Exceptions;
namespace src.Applications.Domains;
/// <summary>
/// 従業員を表すドメインオブジェクト
/// </summary>
public class Employee
{
    public int? Id { get; private set; } // 社員Id
    public string Name { get; private set; } = string.Empty; // 氏名
    public Department Department { get; private set; } // 所属部署
    public string Birthday { get; private set; }
    public string Gender { get; private set; }
    public string PhoneNumber { get; private set;}
    public string Email {get; private set;}
    public string Address {get; private set; }
    public bool DeleteFlag {get; private set;}

    private const int EmpNameMaxLength = 10;
    private const int PhoneNumberMaxLength = 20;
    private const int EmailMaxLength = 100;
    private const int AddressMaxLength = 100;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="id">社員Id</param>
    /// <param name="name">氏名</param>
    /// <param name="department">所属部署</param>
    public Employee(int? id, string name, Department department, string birthday, string gender, string phoneNumber, string email, string address, bool deleteflag){
        ValidateName(name);
        Id = id;
        Name = name;
        Department = department;
        Birthday = birthday;
        Gender = gender;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        DeleteFlag = deleteflag;
    }

    /// <summary>
    /// ID未定の社員を作成する場合のコンストラクタ
    /// </summary>
    /// <param name="name">氏名</param>
    /// <param name="department">所属部署</param>
    public Employee(string name, Department department)
        : this(null, name, department) { }

    /// <summary>
    /// 氏名の検証
    /// </summary>
    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > EmpNameMaxLength)
            throw new DomainException($"氏名は1文字以上{EmpNameMaxLength}文字以内で入力してください");
    }
    /// <summary>
    /// 生年月日の検証
    /// </summary>
    private DateTime ValidateBirthday(string birthday)
    {
        if (string.IsNullOrWhiteSpace(birthday)){
            throw new DomainException("生年月日は必須です");
        }
        if (DateTime.TryParseExact(birthday,"yyyymmdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            throw new DomainException("生年月日が不正です");
        }

        return result;
    }
    /// <summary>
    /// 電話番号の検証
    /// </summary>
    private void ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > EmpNameMaxLength)
            throw new DomainException($"氏名は1文字以上{EmpNameMaxLength}文字以内で入力してください");
    }
    /// <summary>
    /// メールアドレスの検証
    /// </summary>
    private void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > EmpNameMaxLength)
            throw new DomainException($"氏名は1文字以上{EmpNameMaxLength}文字以内で入力してください");
    }
    /// <summary>
    /// 電話番号の検証
    /// </summary>
    private void ValidateAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > EmpNameMaxLength)
            throw new DomainException($"氏名は1文字以上{EmpNameMaxLength}文字以内で入力してください");
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
        => $"{Id?.ToString() ?? "未登録"}: {Name} / {Department?.Name ?? "未配属"}";
}