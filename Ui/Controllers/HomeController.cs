using BL.Contracts;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ui.Models;

namespace Ui.Controllers
{

    [Authorize(Roles = "Admin")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShippingType _shippingType;

        public HomeController(ILogger<HomeController> logger, IShippingType shippingType)
        {
            _logger = logger;
            _shippingType = shippingType;
        }

        public IActionResult Index()
        {
            var type = _shippingType.GetAll();
            return View();
        }

        //public iactionresult privacy()
        //{
        //    return view();
        //}

        //[responsecache(duration = 0, location = responsecachelocation.none, nostore = true)]
        //public iactionresult error()
        //{
        //    return view(new errorviewmodel { requestid = activity.current?.id ?? httpcontext.traceidentifier });
        //}
    }
}
