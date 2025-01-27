using Company.G01.PL.ViewModels;
//using Company.G01.PL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Company.G01.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IScopedService _scoped01;
        //private readonly IScopedService _scoped02;
        //private readonly ISingeltonService _singelton01;
        //private readonly ISingeltonService _singelton02;
        //private readonly ITransientService _transient01;
        //private readonly ITransientService _transient02;

        public HomeController(
            ILogger<HomeController> logger
            //IScopedService scoped01,
            //IScopedService scoped02,
            //ISingeltonService singelton01,
            //ISingeltonService singelton02,
            //ITransientService transient01,
            //ITransientService transient02
            )
        {
            _logger = logger;
            //_scoped01 = scoped01;
            //_scoped02 = scoped02;
            //_singelton01 = singelton01;
            //_singelton02 = singelton02;
            //_transient01 = transient01;
            //_transient02 = transient02;
        }
        // /Home/TestLifeTime
        //public string TestLifeTime()
        //{
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append($"scoped01 :: {_scoped01.GetGuid()}\n");
        //    builder.Append($"scoped02 :: {_scoped02.GetGuid()}\n\n");

        //    builder.Append($"transient01 :: {_transient01.GetGuid()}\n");
        //    builder.Append($"transient02 :: {_transient02.GetGuid()}\n\n");

        //    builder.Append($"singelton01 :: {_singelton01.GetGuid()}\n");
        //    builder.Append($"singelton02 :: {_singelton02.GetGuid()}\n\n");

        //    return builder.ToString();

        //}
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
