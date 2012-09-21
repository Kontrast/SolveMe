using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SolveMe.Models;

namespace SolveMe.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin")]
    public class TaskAdminController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /TaskAdmin/

        public ActionResult Index()
        {
            return View(db.Tasks.Select(t => t).ToList());
        }

        //
        // GET: /TaskAdmin/Details/5

        public ActionResult Details(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // GET: /TaskAdmin/Create

        public ActionResult Create()
        {
            ViewBag.Author = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /TaskAdmin/Create

        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Author = new SelectList(db.UserProfiles, "UserId", "UserName", task.Author);
            return View(task);
        }

        //
        // GET: /TaskAdmin/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.Author = new SelectList(db.UserProfiles, "UserId", "UserName", task.Author);
            return View(task);
        }

        //
        // POST: /TaskAdmin/Edit/5

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Author = new SelectList(db.UserProfiles, "UserId", "UserName", task.Author);
            return View(task);
        }

        //
        // GET: /TaskAdmin/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        //
        // POST: /TaskAdmin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            var deleteList = db.Solutions.Where(t => t.TaskId == task.TaskId).Select(t => t).ToList();
            foreach (var solution in deleteList)
            {
                db.Solutions.Remove(solution);
            }
            task.UserProfiles.Clear();
            task.UserProfiles1.Clear();
            task.Categories.Clear();
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}