using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Applications.Domains;
namespace src.Presentations.ViewModels;
/// <summary>
/// 部署更新ViewModelクラス
/// </summary>
///
/// コンストラクタ
/// 
public class UserLoginViewModel
{
    /// <summary>
    /// 選択された部署名
    /// </summary>
    [Display(Name = "ログインID")]
    [Required(ErrorMessage = "入力してください")]
    public string? Id { get; set; }

    /// <summary>
    /// 選択された部署名
    /// </summary>
    [Display(Name = "パスワード")]
    [Required(ErrorMessage = "入力してください")]
    public string? Password { get; set; }


    /// 
    /// コンストラクタ
    /// 
    public UserLoginViewModel()
    {
        Id = "user";
        Password = "password";
    }

    public override string ToString()
    {
        return $"Id={Id}, Password={Password}";
    }
}