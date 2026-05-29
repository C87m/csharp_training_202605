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
public class UserRegisterViewModelAdapter : IRestorer<Login, UserRegisterViewModel>
{
    /// <summary>
    /// UserLoginViewModelをドメインオブジェクト:Departmentに変換する
    /// </summary>
    /// <param name="target">UserLoginViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public Login Restore(UserRegisterViewModel target)
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
    public List<UserRegisterViewModel> Convert(List<Login> domain)
    {
        List<UserRegisterViewModel> users = [];
        // 表示するDepartment(部署)を作成する
        foreach(Login user in domain)
        {
            var viewModel = new UserRegisterViewModel(user);
            users.Add(viewModel);
        }
        return users;
    }

    /// <summary>
    /// ドメインオブジェクト:DepartmentをDepartmentRegisterViewModelに変換する
    /// </summary>
    /// <param name="target">DepartmentRegisterViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public UserRegisterViewModel Convert(Login domain)
    {
        UserRegisterViewModel user = new(domain);
        
        return user;
    }
}