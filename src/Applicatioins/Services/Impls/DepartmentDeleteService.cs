using src.Applications.Repositories;
using src.Applications.Domains;
using src.Exceptions;
using src.Infrastructures.Context;
namespace src.Applications.Services.Impls;
/// <summary>
/// 従業員登録サービスインターフェイスの実装
/// </summary>
public class DepartmentDeleteService : IDepartmentDeleteService
{

    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインオブジェクト:部署のCRUD操作インターフェイス
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;
    /// <summary>
    /// ドメインオブジェクト:部署のCRUD操作インターフェイス
    /// </summary>
    private readonly IEmployeeRepository _employeeRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context">アプリケーション用DbContext</param>
    /// <param name="departmentRepository">部署のCRUD操作インターフェイス</param>
    public DepartmentDeleteService(
        AppDbContext context,
        IDepartmentRepository departmentRepository,
        IEmployeeRepository employeeRepository)
    {
        _context = context;
        _departmentRepository = departmentRepository;
        _employeeRepository = employeeRepository;
    }

    /// <summary>
    /// すべての部署を取得する
    /// </summary>
    /// <returns></returns>
    public List<Department> GetDepartments()
    {
        return _departmentRepository.FindAll();
    }
    /// <summary>
    /// 指定したIDの部署を取得する
    /// </summary>
    /// <returns></returns>
    public Department GetDepartmentById(int id)
    {
        var department = _departmentRepository.FindById(id);
        if (department == null)
        {
            throw new InternalException($"部署{id}が見つかりませんでした。");
        }
        return department;
    }

    /// <summary>
    /// 指定したIDの部署に所属する人数を返す
    /// </summary>
    /// <returns></returns>
    public List<Employee> GetDeptMemberById(int id)
    {
        List<Employee> member = _employeeRepository.FindMember(id);
        return member;
    }

    /// <summary>
    /// 部署を削除する
    /// </summary>
    /// <param name="department"></param>
    public void DepartmentDelete(Department department)
    {
        var deptMember = GetDeptMemberById((int)department.Id!);
        // 部署に所属している人を探して部署を無所属にする
        if (deptMember.Count > 0)
        {
            foreach(Employee member in deptMember)
            {
                try{
                    // トランザクションの開始
                    _context.Database.BeginTransaction();

                    member.ChangeDepartment(_departmentRepository.FindById(1));
                    // 従業員の登録
                    _employeeRepository.Renew(member);

                    // トランザクションのコミット
                    _context.Database.CommitTransaction();
                }
                catch
                {
                    // トランザクションのロールバック
                    _context.Database.RollbackTransaction();
                    throw;
                }
            }
        }

        try
        {
            // トランザクションの開始
            _context.Database.BeginTransaction();
            // 従業員の登録
            _departmentRepository.Delete(department);
            // トランザクションのコミット
            _context.Database.CommitTransaction();   
        }
        catch
        {
            // トランザクションのロールバック
            _context.Database.RollbackTransaction();
            throw;
        }
    }
}