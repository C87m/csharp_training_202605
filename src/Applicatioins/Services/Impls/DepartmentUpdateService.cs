using src.Applications.Repositories;
using src.Applications.Domains;
using src.Exceptions;
using src.Infrastructures.Context;
namespace src.Applications.Services.Impls;
/// <summary>
/// 従業員登録サービスインターフェイスの実装
/// </summary>
public class DepartmentUpdateService : IDepartmentUpdateService
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
    /// コンストラクタ
    /// </summary>
    /// <param name="context">アプリケーション用DbContext</param>
    /// <param name="departmentRepository">部署のCRUD操作インターフェイス</param>
    public DepartmentUpdateService(
        AppDbContext context,
        IDepartmentRepository departmentRepository)
    {
        _context = context;
        _departmentRepository = departmentRepository;
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
    /// 部署を更新する
    /// </summary>
    /// <returns></returns>
    public void DepartmentUpdate(Department department)
    {
        try
        {
            // トランザクションの開始
            _context.Database.BeginTransaction();
            // 従業員の登録
            _departmentRepository.Renew(department);
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