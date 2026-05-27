using src.Applications.Adapters;
using src.Applications.Domains;
using src.Presentations.ViewModels;
namespace src.Presentations.Adapters;
/// <summary>
/// UserLoginViewModel(部署登録ViewModel)を
/// ドメインオブジェクト:Departmentに変換するアダプターインターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Department</typeparam>
/// <typeparam name="TTarget">UserLoginForm</typeparam>
public class UserLoginViewModelAdapter : IRestorer<Login, UserLoginViewModel>
{
    /// <summary>
    /// UserLoginViewModelをドメインオブジェクト:Departmentに変換する
    /// </summary>
    /// <param name="target">UserLoginViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public Login Restore(UserLoginViewModel target)
    {
        // 登録するDepartment(部署)を作成する
        var user = new Login(target.Id, target.Password);
        return user;
    }

    /// <summary>
    /// ドメインオブジェクト:DepartmentをDepartmentRegisterViewModelに変換する
    /// </summary>
    /// <param name="target">DepartmentRegisterViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public List<UserLoginViewModel> Convert(List<Login> domain)
    {
        List<UserLoginViewModel> users = [];
        // 表示するDepartment(部署)を作成する
        foreach(Login user in domain)
        {
            var viewModel = new UserLoginViewModel(user);
            users.Add(viewModel);
        }
        return users;
    }

    /// <summary>
    /// ドメインオブジェクト:DepartmentをDepartmentRegisterViewModelに変換する
    /// </summary>
    /// <param name="target">DepartmentRegisterViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public UserLoginViewModel Convert(Login domain)
    {
        UserLoginViewModel user = new(domain);
        
        return user;
    }
}