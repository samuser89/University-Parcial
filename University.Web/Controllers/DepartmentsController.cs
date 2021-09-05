using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace University.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }
    }
}