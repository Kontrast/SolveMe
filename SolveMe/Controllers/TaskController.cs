using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SolveMe.Filters;
using SolveMe.Models;
using WebMatrix.WebData;
using Newtonsoft.Json;

namespace SolveMe.Controllers
{

    [InitializeSimpleMembership]
    public class TaskController : Controller
    {
        private readonly DataAccessModel data = new DataAccessModel();

        //
        // GET: /Task/Create
        [Authorize]
        public ActionResult Create()
        {
            return View(new TaskViewModel());
        }

        //
        // POST: /Task/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(TaskViewModel task)
        {
            Task newTask = new Task
                               {
                                   TaskText = EscapeHtml(task.TaskText),
                                   Title = EscapeHtml(task.Title),
                                   Added = DateTime.Now,
                                   Author = WebSecurity.CurrentUserId,
                                   UserProfile = data.FindUser(WebSecurity.CurrentUserId)
                               };

            var categoryNames = JsonConvert.DeserializeObject<List<string>>(JsonConvert.DeserializeObject<string>(task.Categories));
            foreach (string s in categoryNames)
            {
                data.AddCategory(s);
                newTask.Categories.Add(data.GetCategory(s));
            }
            var solutions = task.Answers.Split(',');
            foreach (var solution in solutions)
            {
                data.AddAnsver(solution);
            }
            data.modifiUserProfileSuccess();
            data.AddTask(newTask);
            return RedirectToAction("Index", "Home");
        }

        private string EscapeHtml(string source)
        {
            string step1 = source.Replace("<", "&lt;");
            string step2 = step1.Replace(">", "&gt;");
            return step2.Replace("\"", "&quot;");
        }

        [Authorize]
        public JsonResult GetCategoriesJson()
        {
            return Json(data.GetCategoriesNames(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult FilterCategories(string q)
        {
            var tags = data.GetCategoriesNames().Where(t => t.Contains(q)).ToList();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Statistics()
        {

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            data.Dispose();
            base.Dispose(disposing);
        }

        [Authorize]
        public string GetDataDiagram()
        {
            int userId = WebSecurity.GetUserId(User.Identity.Name);
            DateTime[] createTasks = data.GetTaskCrearedDates(userId);

            DiagramModel diagram = new DiagramModel();
            diagram.name = "Users task activity";
            // buf[i][0] - x
            // buf[i][1] - y
            int length = createTasks.Count();
            long[][] buf = new long[length][];
            for (int i = 0; i < length; i++)
            {
                buf[i] = new long[2];
                TimeSpan elapsedSpan = new TimeSpan(createTasks[i].ToLocalTime().ToUniversalTime().Ticks);
                buf[i][0] = (long)elapsedSpan.TotalMilliseconds;
                buf[i][1] = i + 1;
            }
            diagram.data = buf;

            return JsonConvert.SerializeObject(new Object[] { diagram });// need return array of diagrams
        }
    }
}