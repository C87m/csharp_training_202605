using src.Exceptions;
using System.Linq;
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
    private const int PhoneNumberMaxLength = 11;
    private const int EmailMaxLength = 100;
    private const int AddressMaxLength = 100;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="id">社員Id</param>
    /// <param name="name">氏名</param>
    /// <param name="department">所属部署</param>
    public Employee(int? id, string name, Department department, string birthday, string gender, string phoneNumber, string email, string address, bool deleteFlag = false){
        ValidateName(name);
        ValidateBirthday(birthday);
        ValidatePhoneNumber(phoneNumber);
        ValidateEmail(email);
        ValidateAddress(address);

        Id = id;
        Name = name;
        Department = department;
        Birthday = birthday;
        Gender = gender;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        DeleteFlag = deleteFlag;
    }

    /// <summary>
    /// ID未定の社員を作成する場合のコンストラクタ
    /// </summary>
    /// <param name="name">氏名</param>
    /// <param name="department">所属部署</param>
    public Employee(string name, Department department, string birthday, string gender, string phoneNumber, string email, string address, bool deleteFlag=false)
        : this(null, name, department, birthday, gender, phoneNumber, email, address, deleteFlag) { }

    /// <summary>
    /// 氏名の検証
    /// </summary>
    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > EmpNameMaxLength)
        {
            throw new DomainException($"氏名は1文字以上{EmpNameMaxLength}文字以内で入力してください");
        }
    }
    /// <summary>
    /// 生年月日の検証
    /// </summary>
    private void ValidateBirthday(string birthday)
    {
        if (string.IsNullOrWhiteSpace(birthday)){
            throw new DomainException("生年月日は必須です");
        }
        if (birthday.All(char.IsDigit)) // 日付型には後でします
        {
            throw new DomainException("電話番号が不正です");
        }
        
    }
    /// <summary>
    /// 電話番号の検証
    /// </summary>
    private void ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new DomainException("電話番号は必須です");
        }
        if (phoneNumber.All(char.IsDigit))
        {
            throw new DomainException("電話番号が不正です");
        }
        if (phoneNumber.Length > PhoneNumberMaxLength)
        {
            throw new DomainException($"電話番号が不正です");
        }

    }
    /// <summary>
    /// メールアドレスの検証
    /// </summary>
    private void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new DomainException("メールアドレスは必須です");
        }
        if (!email.Contains("@"))
        {
            throw new DomainException($"メールアドレスが不正です");
        }
        if (email.Length > EmailMaxLength)
        {
            throw new DomainException($"メールアドレスが不正です");
        }
    }
    /// <summary>
    /// 住所の検証
    /// </summary>
    private void ValidateAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new DomainException("住所は必須です");
        }
        if (address.Length > AddressMaxLength)
        {
            throw new DomainException($"住所が不正です");
        }
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
    /// 生年月日を変更する
    /// </summary>
    public void ChangeBirthday(Department? department)
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