using src.Applications.Repositories;
using src.Applications.Domains;
using src.Exceptions;
using src.Infrastructures.Context;
namespace src.Applications.Services.Impls;
/// <summary>
/// 従業員登録サービスインターフェイスの実装
/// </summary>
public class EmployeeDeleteService : IEmployeeDeleteService
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
    public EmployeeDeleteService(
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
    /// すべての部署を取得する
    /// </summary>
    /// <returns></returns>
    public List<Employee> GetEmployees()
    {
        return _employeeRepository.FindAll();
    }
    /// <summary>
    /// 指定したIDの社員を取得する
    /// </summary>
    /// <returns></returns>
    public Employee GetEmployeeById(int id)
    {
        var employee = _employeeRepository.FindById(id);
        if (employee == null)
        {
            throw new InternalException($"社員{id}が見つかりませんでした。");
        }
        return employee;
    }

    /// <summary>
    /// 部署を削除する
    /// </summary>
    /// <param name="department"></param>
    public void EmployeeDelete(Employee employee)
    {
        try
        {
            // トランザクションの開始
            _context.Database.BeginTransaction();
            // 従業員の登録
            _employeeRepository.Delete(employee);
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