using Microsoft.AspNetCore.Mvc;
using src.Applications.Services;
using src.Presentations.ViewModels;

namespace src.Presentations.Controllers;
public class DepartmentShowController
{
    [Route("DepartmentShow")]
    public class RazorSyntaxSampleController : Controller
    {
        /// <summary>
        /// ViewModel SampleFormのListをRazor View Show.cshtmlに渡す
        /// </summary>
        /// <returns></returns>
        [HttpGet("ShowDept")]
        public ActionResult Enter()
        {
            return View();
        }
    }
}
