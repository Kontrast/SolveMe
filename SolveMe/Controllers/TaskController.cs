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
            List<DateTime> createdTasks = data.GetTaskCrearedDates(WebSecurity.GetUserId(User.Identity.Name));
            DiagramModel diagram = new DiagramModel
            {
                name = "Users task activity",
                data = new List<List<object>>()
            };
            int coordinateX = 0;
            foreach (var createdTask in createdTasks)
            {
                List<object> temp = new List<object>();
                temp.Add(new TimeSpan(createdTask.ToLocalTime().ToUniversalTime().Ticks).TotalMilliseconds);
                temp.Add(coordinateX++);
                diagram.data.Add(temp);
            }
            return JsonConvert.SerializeObject(new List<object> { diagram });
        }
    }
}