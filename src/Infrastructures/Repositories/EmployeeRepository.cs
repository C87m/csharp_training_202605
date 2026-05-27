using src.Infrastructures.Context;
using src.Applications.Domains;
using src.Applications.Repositories;
using src.Infrastructures.Adapters;
using src.Exceptions;
using Microsoft.EntityFrameworkCore;
namespace src.Infrastructures.Repositories;
/// <summary>
/// ドメインオブジェクト:従業員のCRUD操作インターフェイスの実装
/// </summary>
public class EmployeeRepository : IEmployeeRepository
{
    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインモデル:従業員と従業員エンティティの相互変換インターフェイスの実装
    /// </summary>
    private readonly EmployeeEntityAdapter _adapter;
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context"></param>
    /// <param name="adapter"></param>
    public EmployeeRepository(AppDbContext context, EmployeeEntityAdapter adapter)
    {
        _context = context;
        _adapter = adapter;
    }

    /// <summary>
    /// すべての社員を取得する
    /// </summary>
    /// <returns>社員のリスト</returns>
    public List<Employee> FindAll()
    {
        try
        {
            var entities = _context.Employees.Where(i=>i.DeleteFlag==false).ToList();
            var results = new List<Employee>();
            foreach (var entity in entities)
            {
                results.Add(_adapter.Restore(entity));
            }   
            return results;
        }
        catch (Exception e)
        {
            throw new InternalException(
                "すべての部署を取得できませんでした。", e);
        }
    }
    /// <summary>
    /// すべての社員を取得する
    /// </summary>
    /// <returns>社員のリスト</returns>
    public Employee FindById(int id)
    {
        try
        {
            var result = _context.Employees.FirstOrDefault(d => d.EmpId == id);
            if (result == null)
            {
                throw new InternalException(
                "指定された部署Idの部署を取得できませんでした。");
            }
            return _adapter.Restore(result);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "指定された部署Idの部署を取得できませんでした。", e);
        }
    }
    /// <summary>
    /// すべての社員を取得する
    /// </summary>
    /// <returns>社員のリスト</returns>
    public List<int?> FindAllDept()
    {
        try
        {
            var entities = _context.Employees.Where(i=>i.DeleteFlag==false).ToList();
            var results = new List<int?>();
            foreach (var entity in entities)
            {
                results.Add(entity.DeptId);
            }   
            return results;
        }
        catch (Exception e)
        {
            throw new InternalException(
                "すべての部署を取得できませんでした。", e);
        }
    }

    /// <summary>
    /// 指定された部署Idの部署の人数取得する
    /// </summary>
    /// <param name="id">部署Id</param>
    /// <returns>取得して部署</returns>
    public List<Employee> FindMember(int id)
    {
        var entities = _context.Employees.Where(i=>i.DeleteFlag==false).Where(i => i.DeptId==id).ToList();
            var results = new List<Employee>();
            foreach (var entity in entities)
            {
                results.Add(_adapter.Restore(entity));
            }   
            return results;
    }

    /// <summary>
    /// Idから部署Idを取得する
    /// </summary>
    /// <returns>社員のリスト</returns>
    public int FindDeptIdById(int id)
    {

        return (int)_context.Employees.FirstOrDefault(d => d.EmpId == id)!.DeptId!;
    }

    /// <summary>
    /// 従業員を永続化する
    /// </summary>
    /// <param name="employee">永続化対象の従業員</param>
    public void Create(Employee employee)
    {
        try
        {
            var entity = _adapter.Convert(employee);
            _context.Employees.Add(entity);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new InternalException(
                "従業員の永続化ができませんでした。", e);
        }
    }

    /// <summary>
    /// 部署を更新する
    /// </summary>
    /// <param name="department">更新対象の部署</param>
    public void Renew(Employee employee)
    {
        
        var existingEntity = _context.Employees.FirstOrDefault(d => d.EmpId == employee.Id);
        if(existingEntity == null)
        {
            throw new InternalException($"社員が見つかりません。");
        }

        try
        {
            existingEntity.EmpName = employee.Name;
            existingEntity.DeptId = employee.Department!.Id;
            existingEntity.Birthday = employee.Birthday;
            existingEntity.Gender = employee.Gender;
            existingEntity.PhoneNumber = employee.PhoneNumber;
            existingEntity.Email = employee.Email;
            existingEntity.Address = employee.Address;
            _context.SaveChanges();
            
        }
        catch (Exception e)
        {
            throw new InternalException("社員の更新ができませんでした。", e);
        }
    }

    /// <summary>
    /// 部署を削除する
    /// </summary>
    /// <param name="department">更新対象の部署</param>
    public void Delete(Employee employee)
    {
        var existingEntity = _context.Employees.FirstOrDefault(d => d.EmpId == employee.Id);
        if(existingEntity == null)
        {
            throw new InternalException($"社員が見つかりません。");
        }

        try
        {
            existingEntity.DeleteFlag = true;
            _context.SaveChanges();
            
        }
        catch (Exception e)
        {
            throw new InternalException("社員の削除ができませんでした。", e);
        }
    }
}