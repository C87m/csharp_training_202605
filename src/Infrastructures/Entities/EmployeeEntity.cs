using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace src.Infrastructures.Entities;
/// <summary>
/// 従業員テーブル(employee)を扱うEntity Framework Coreのエンティティクラス
/// </summary>
[Table("employee")]
public class EmployeeEntity
{
    /// <summary>
    /// 従業員Id(主キー)
    /// </summary>
    [Key]
    [Column("id")]
    public int EmpId { get; set; }
    [Column("name")]
    /// <summary>
    /// 従業員名
    /// </summary>
    public string EmpName { get; set; } = string.Empty;
    /// <summary>
    /// 所属部署Id(外部キー)
    /// </summary>
    [Column("dept_id")]
    public int? DeptId { get; set; }
    /// <summary>
    /// 生年月日
    /// </summary>
    [Column("birthday")]
    public string? Birthday { get; set; }
    /// <summary>
    /// 性別
    /// </summary>
    [Column("gender")]
    public string Gender { get; set; } = string.Empty;
    /// <summary>
    /// 電話番号
    /// </summary>
    [Column("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;
    /// <summary>
    /// メールアドレス
    /// </summary>
    [Column("email")]
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// 住所
    /// </summary>
    [Column("address")]
    public string Address { get; set; } = string.Empty;
    /// <summary>
    /// 削除フラグ
    /// </summary>
    [Column("deleteFlag")]
    public bool DeleteFlag { get; set; } = false;
}