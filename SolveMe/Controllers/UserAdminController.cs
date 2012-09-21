using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SolveMe.Models;

namespace SolveMe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserAdminController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /UserAdmin/
        
        public ActionResult Index()
        {
            var userprofiles = db.UserProfiles.Include(u => u.Sex);
            return View(userprofiles.ToList());
        }

        //
        // GET: /UserAdmin/Details/5

        public ActionResult Details(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // GET: /UserAdmin/Create

        public ActionResult Create()
        {
            ViewBag.Gender = new SelectList(db.Sexes, "SexId", "Value");
            return View();
        }

        //
        // POST: /UserAdmin/Create

        [HttpPost]
        public ActionResult Create(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Gender = new SelectList(db.Sexes, "SexId", "Value", userprofile.Gender);
            return View(userprofile);
        }

        //
        // GET: /UserAdmin/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gender = new SelectList(db.Sexes, "SexId", "Value", userprofile.Gender);
            return View(userprofile);
        }

        //
        // POST: /UserAdmin/Edit/5

        [HttpPost]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Gender = new SelectList(db.Sexes, "SexId", "Value", userprofile.Gender);
            return View(userprofile);
        }

        //
        // GET: /UserAdmin/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /UserAdmin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            userprofile.Tasks.Clear();
            userprofile.Tasks2.Clear();
            userprofile.webpages_Roles.Clear();
            db.UserProfiles.Remove(userprofile);
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