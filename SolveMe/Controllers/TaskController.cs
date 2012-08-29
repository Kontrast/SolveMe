using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolveMe.Models;

namespace SolveMe.Controllers
{
    public class TaskController : Controller
    {
        //
        // GET: /Task/
        [ValidateInput(false)]
        public ActionResult NewTask()
        {
            return View();
        }

    }
}
