using System.Data;
using System.Web.Mvc;
using SolveMe.Filters;
using SolveMe.Models;
using WebMatrix.WebData;

namespace SolveMe.Controllers
{
    [InitializeSimpleMembership]
    public class ProfileController : Controller
    {
        private readonly Entities db = new Entities();
      
        //
        // GET: /Profile/Create

        public ActionResult Create()
        {
            ViewBag.Gender = new SelectList(db.Sexes, "SexId", "Value");
            return View();
        }

        //
        // POST: /Profile/Create

        [HttpPost]
        public ActionResult Create(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.Gender = new SelectList(db.Sexes, "SexId", "Value", userprofile.Gender);
            return View(userprofile);
        }

        //
        // GET: /Profile/Edit/5

        public ActionResult Edit()
        {
            UserProfile userprofile = db.UserProfiles.Find(WebSecurity.CurrentUserId);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gender = new SelectList(db.Sexes, "SexId", "Value", userprofile.Gender);
            return View(userprofile);
        }

        public ActionResult Display(int id)
        {
            return View(db.UserProfiles.Find(id));
        }
        
        //
        // POST: /Profile/Edit/5

        [HttpPost]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            { 
                UserProfile user = db.UserProfiles.Find(WebSecurity.CurrentUserId);
                user.FirstName = userprofile.FirstName;
                user.LastName = userprofile.LastName;
                user.DateOfBirth = userprofile.DateOfBirth;
                user.Sex = userprofile.Sex;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            ViewBag.Gender = new SelectList(db.Sexes, "SexId", "Value", userprofile.Gender);
            return View(userprofile);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}