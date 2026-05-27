using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using src.Infrastructures.Context;
using src.Applications.Domains;
using src.Applications.Repositories;
using src.Infrastructures.Adapters;
using src.Exceptions;
using src.Infrastructures.Repositories;

namespace tests.TestProject.Infrastructures.Repositories;

[DoNotParallelize]
[TestClass]
public class DepartmentRepositoryTests
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=employee_manager;Username=postgres;Password=training;";

    private DepartmentRepository _repository = null!;
    private AppDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var departmentAdapter = new DepartmentEntityAdapter();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        _context = new AppDbContext(options);

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "employee_manager.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new DepartmentRepository(_context, departmentAdapter);
    }

    [TestMethod("部署全件検索")]
    public void FindAll_Result()
    {
        var actual = _repository.FindAll();

        AreEqual(3, actual.Count);
        IsTrue(actual.Any(c => c.Equals(new Department(1, "無所属"))));
        IsTrue(actual.Any(c => c.Equals(new Department(2, "総務部"))));
        IsTrue(actual.Any(c => c.Equals(new Department(3, "経理部"))));
    }

    [TestMethod("存在する部署IDを検索")]
    public void FindById_WhenIdCorrect()
    {
        var expected = new Department(1, "無所属");
        var actual = _repository.FindById(1);

        AreEqual(expected, actual);
        AreEqual("無所属", actual?.Name);
    }

    [TestMethod("存在しない部署IDを検索")]
    public void FindById_WhenIdNotFound()
    {
        var exception = ThrowsException<InternalException>(
                () => _repository.FindById(999)); 
        AreEqual("指定された部署Idの部署を取得できませんでした。", exception.Message);
    }

    [TestMethod("部署登録完了")]
    public void Create_Success()
    {
        var newDepartment = new Department(null, "システム開発部");
        _repository.Create(newDepartment);

        var exception = new Department(4, "システム開発部");
        AreEqual(exception, _repository.FindById(4));
    }

    [TestMethod("部署登録失敗")]
    public void Create_Failed()
    {
        var newDepartment = new Department(1, "システム開発部");
        var exception = ThrowsException<InternalException>(
                () => _repository.Create(newDepartment)); 
        AreEqual("部署の永続化ができませんでした。", exception.Message);
    }

    // 更新
    [TestMethod("部署更新成功")]
    public void Renew_Success()
    {
        var newDepartment = new Department(2, "技術部");
        _repository.Renew(newDepartment);
        
        var exception = new Department(2, "技術部");
        AreEqual(exception, _repository.FindById(2));
    }

    [TestMethod("部署更新失敗：部署検索失敗")]
    public void Renew_Failed_NotFoundDepartment()
    {
        var newDepartment = new Department(null, "システム開発部");
        var exception = ThrowsException<InternalException>(
                () => _repository.Renew(newDepartment)); 
        AreEqual("部署が見つかりません。", exception.Message);
    }

    [TestMethod("部署更新失敗：nullに変更")]
    public void Renew_Failed_UpdateToNull()
    {
        var newDepartment = new Department(2, null);
        var exception = ThrowsException<InternalException>(
                () => _repository.Renew(newDepartment)); 
        AreEqual("部署の更新ができませんでした。", exception.Message);
    }

    // 削除
    [TestMethod("部署削除成功")]
    public void Delete_Success()
    {
        var department = _repository.FindById(3);
        _repository.Delete(department!);
        var exception = ThrowsException<InternalException>(
                () => _repository.FindById(3)); 
        AreEqual("部署が見つかりません。", exception.Message);
    }
    
    [TestMethod("部署削除失敗：社員が存在")]
    public void Delete_Failed_HasMember()
    {
        var department = new Department(2, "総務部");
        var exception = ThrowsException<InternalException>(
                () => _repository.Delete(department)); 
        AreEqual("部署の削除ができませんでした。", exception.Message);
    }

    [TestMethod("部署削除失敗：部署検索失敗")]
    public void Delete_Failed_NotFoundDepartment()
    {
        var newDepartment = new Department(null, "システム開発部");
        var exception = ThrowsException<InternalException>(
                () => _repository.Delete(newDepartment)); 
        AreEqual("部署が見つかりません。", exception.Message);
    }
}
