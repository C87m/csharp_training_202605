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
        IsTrue(actual.Any(c => c.Equals(new Department(1, "総務部"))));
        IsTrue(actual.Any(c => c.Equals(new Department(2, "経理部"))));
        IsTrue(actual.Any(c => c.Equals(new Department(3, "人事部"))));
    }

    [TestMethod("存在する部署IDを検索")]
    public void FindById_WhenIdCorrect()
    {
        var expected = new Department(1, "総務部");
        var actual = _repository.FindById(1);

        AreEqual(expected, actual);
        AreEqual("総務部", actual?.Name);
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
}
