using src.Applications.Repositories;
using src.Applications.Domains;
using src.Exceptions;
using src.Infrastructures.Context;
namespace src.Applications.Services.Impls;
/// <summary>
/// 従業員登録サービスインターフェイスの実装
/// </summary>
public class EmployeeShowService : IEmployeeShowService
{

    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインオブジェクト:社員のCRUD操作インターフェイス
    /// </summary>
    private readonly IEmployeeRepository _EmployeeRepository;
    /// <summary>
    /// ドメインオブジェクト:部署のCRUD操作インターフェイス
    /// </summary>
    private readonly IDepartmentRepository _DepartmentRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context">アプリケーション用DbContext</param>
    /// <param name="EmployeeRepository">部署のCRUD操作インターフェイス</param>
    /// <param name="DepartmentRepository">部署のCRUD操作インターフェイス</param>
    public EmployeeShowService(
        AppDbContext context,
        IEmployeeRepository EmployeeRepository,
        IDepartmentRepository DepartmentRepository)
    {
        _context = context;
        _EmployeeRepository = EmployeeRepository;
        _DepartmentRepository = DepartmentRepository;
    }

    /// <summary>
    /// すべての部署を取得する
    /// </summary>
    /// <returns></returns>
    public List<Department> GetDepartments()
    {
        return _DepartmentRepository.FindAll();
    }
    /// <summary>
    /// 指定された部署Idの部署を取得する
    /// </summary>
    /// <param name="id">部署Id</param>
    /// <returns></returns>
    public Department GetById(int id)
    {
        var result = _DepartmentRepository.FindById(id)!;
        if (result == null)
        {
            throw new NotFoundException($"部署Id{id}に該当する部署は存在しません");
        }
        return result;
    }

    /// <summary>
    /// すべての社員を取得する
    /// </summary>
    /// <returns></returns>
    public List<Employee> GetEmployees()
    {
        List<Employee> EmpList = _EmployeeRepository.FindAll();
        List<int?> DeptList = _EmployeeRepository.FindAllDept();
        foreach(var item in EmpList.Zip(DeptList, (emp, dept) => new {Emp=emp, Dept=dept }))
        {
            item.Emp.ChangeDepartment(_DepartmentRepository.FindById((int)item.Dept!));
        }
        return EmpList;
    }

}