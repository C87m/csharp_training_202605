using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Applications.Domains;

namespace src.Presentations.ViewModels;

public class DepartmentShowViewModel
{
    /// <summary>
    /// 部署IDプロパティ
    /// </summary>
    /// <value></value>
    public int Id { get; set; }
    /// <summary>
    /// 部署名プロパティ
    /// </summary>
    /// <value></value>
    public string? Name { get; set; }
}

