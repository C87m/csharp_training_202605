using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace src.Infrastructures.Entities;
/// <summary>
/// 部署テーブル(department)を扱うEntity Framework Coreのエンティティクラス
/// </summary>
[Table("login")]
public class LoginEntity
{
    /// <summary>
    /// 部署Id(主キー)
    /// </summary> 
    [Key]
    [Column("login_id")]
    public string LoginId { get; set; } = string.Empty;
    /// <summary>
    /// 部署名
    /// </summary> 
    [Column("login_password")]
    public string Password { get; set; } = string.Empty;
}