using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SolveMe.Filters;
using SolveMe.Models;
using WebMatrix.WebData;

namespace SolveMe.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        private readonly DataAccessModel data = new DataAccessModel();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(SearchModel searchModel)
        {
            return View(data.SearchTasks(searchModel));
        }

        public JsonResult SearchFilter(string query)
        {
            var searchResults = data.ExecuteSqlSearchQuery(FulltextQueries.SelectTaskTitle, query).ToList();
            AutocompleteModel autocomplete = new AutocompleteModel
                                                 {
                                                     data = null,
                                                     query = query,
                                                     suggestions = searchResults
                                                 };
            return Json(autocomplete, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayTask(int id)
        {
            var task = data.FindTask(id);
            if (task == null)
            {
                return View("Error", new ErrorPageModel
                                         {
                                             Title = Messages.TaskErrorTitle,
                                             Message = Messages.TaskErrorMessage,
                                             ShowGotoBack = true
                                         });
            }
            return View(task);
        }

        public ActionResult SearchByTag(string id)
        {
            return View("Search", data.GetSearhByTagModel(id));
        }

        public string GetCloudJson()
        {
            Random randomNumber = new Random();
            string url = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "Home/SearchByTag/";
            var tagsForCloud = new DataAccessModel().GetAllTags().Select(tag => new TagCloudModel
            {
                Text = tag.Name,
                Link = url + tag.Name,
                Weight = randomNumber.Next(7).ToString()
            }).ToList();
            return JsonConvert.SerializeObject(tagsForCloud, Formatting.Indented,
                                          new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [AllowAnonymous]
        public ActionResult SwitchTheme()
        {
            if (HttpContext.Session != null)
            {
                if (HttpContext.Session["darkTheme"] == null)
                {
                    HttpContext.Session["darkTheme"] = true;
                }
                else
                {
                    HttpContext.Session.Remove("darkTheme");
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public JsonResult CheckAnswer(AnswerInfo answerInfo)
        {
            Task task = data.FindTask(answerInfo.TaskId);
            UserProfile user = data.FindUser(WebSecurity.CurrentUserId);
            if (answerInfo.UserId != 0)
            {
                UserProfile author = data.FindUser(answerInfo.UserId);
                if (author.UserId != user.UserId)
                {
                    return CheckAnswerWithoutAuthor(user, task, answerInfo);
                }
                AnswerResult result = new AnswerResult();
                result.Success = false;
                result.Message = Messages.UserAuthor;
                return Json(result);
            }
            return CheckAnswerWithoutAuthor(user, task, answerInfo);
        }

        private JsonResult CheckAnswerWithoutAuthor(UserProfile user, Task task, AnswerInfo answerInfo)
        {
            AnswerResult result = new AnswerResult();
            if (!user.Tasks2.Contains(task))
            {
                if (data.IsAnswerCorrect(answerInfo.TaskId, answerInfo.Answer))
                {
                    data.UserSolveTask(user, task);
                    result.Success = true;
                    result.Message = Messages.UserSuccess;
                }
                else
                {
                    data.UserFailToSolveTask(user, task);
                    result.Success = false;
                    result.Message = Messages.UserFail;
                }
                return Json(result);
            }
            result.Success = false;
            result.Message = Messages.UserAlreadySolve;
            return Json(result);
        }
    }
}