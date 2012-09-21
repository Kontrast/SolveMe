using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebMatrix.WebData;

namespace SolveMe.Models
{
    /// <summary>
    /// Provides model to access the data entities
    /// </summary>
    public class DataAccessModel
    {
        private readonly Entities db = new Entities();
        /// <summary>
        /// Modefied the current user if he succesfuly add a new task
        /// </summary>
        public void modifiUserProfileSuccess()
        {
            UserProfile profile = db.UserProfiles.Find(WebSecurity.CurrentUserId);
            profile.TasksCount++;
            profile.Rating += 10;
            db.Entry(profile).State = EntityState.Modified;
        }
        /// <summary>
        /// Modifi the user if he succesfuly solve the task
        /// </summary>
        /// <param name="user">User who solve the task</param>
        /// <param name="task">Task which was solved</param>
        public void UserSolveTask(UserProfile user, Task task)
        {
            user.SolutionsCount++;
            user.Rating += 50;
            user.Tasks2.Add(task);
            db.Entry(user).State = EntityState.Modified;

            task.UserProfiles1.Add(user);
            task.SolutionsCount++;
            if (task.FirstSolve == null)
            {
                task.FirstSolve = DateTime.Now;
            }
            task.Attempts++;
            db.Entry(task).State = EntityState.Modified;
           
            db.SaveChanges();
        }
        /// <summary>
        /// Modifi user who's attempt to solve was unsuccesfull
        /// </summary>
        /// <param name="user">User who get failed</param>
        /// <param name="task">Task which wasn't solved</param>
        public void UserFailToSolveTask(UserProfile user, Task task)
        {
            user.Rating -= 5;
            db.Entry(user).State = EntityState.Modified;
            task.Attempts++;
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();
        }
        /// <summary>
        /// Find the user entity by id
        /// </summary>
        /// <param name="userId">Id of user who must be find</param>
        /// <returns></returns>
        public UserProfile FindUser(int userId)
        {
            return db.UserProfiles.Find(userId);
        }
        /// <summary>
        /// Gets list of dates of adding tasks
        /// </summary>
        /// <param name="userId">Id of user who added the tasks</param>
        /// <returns></returns>
        public List<DateTime> GetTaskCrearedDates(int userId)
        {
            return db.Tasks.Where(t => t.Author == userId).Select(t => t.Added).OrderBy(t => t).ToList();
        }
        /// <summary>
        /// Gets all existing categories
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetAllTags()
        {
            return db.Categories.Select(t => t);
        }
        /// <summary>
        /// Check is category exists
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <returns></returns>
        public bool IsCategoryExists(string name)
        {
            if (db.Categories.Where(t => t.Name == name).Count() != 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Add new category if category with the same name not exists
        /// </summary>
        /// <param name="name">Name of the category</param>
        public void AddCategory(string name)
        {
            if (db.Categories.Where(t => t.Name == name).Count() == 0)
            {
                db.Categories.Add(new Category {Name = name});
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Get all existing categories names
        /// </summary>
        /// <returns></returns>
        public List<string> GetCategoriesNames()
        {
            return db.Categories.Select(t => t.Name).ToList();
        }
        /// <summary>
        /// Get category id by it's name
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <returns></returns>
        public int GetCategoryId(string name)
        {
            var category = db.Categories.Where(t => t.Name == name);
            return (category as Category).CategoryId;
        }
        /// <summary>
        /// Get category entity by it's name
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <returns></returns>
        public Category GetCategory(string name)
        {
            Category category = db.Categories.Where(t => t.Name == name).FirstOrDefault();
            return category;
        }
        /// <summary>
        /// Add an answer into database
        /// </summary>
        /// <param name="value">Value of the answer</param>
        public void AddAnsver(string value)
        {
            db.Solutions.Add(new Solution { Value = value });
        }
        /// <summary>
        /// Add a task into database
        /// </summary>
        /// <param name="newTask">Etity of the task</param>
        public void AddTask(Task newTask)
        {
            db.Tasks.Add(newTask);
            db.SaveChanges();
        }
        /// <summary>
        /// Find task in database by id
        /// </summary>
        /// <param name="id">Id of the task</param>
        /// <returns></returns>
        public Task FindTask(int id)
        {
            return db.Tasks.Find(id);
        }
        /// <summary>
        /// Get the model whitch represents results of search tasks by tag name
        /// </summary>
        /// <param name="tagName">Tag name</param>
        /// <returns></returns>
        public SearchModel GetSearhByTagModel(string tagName)
        {
            SearchModel search = new SearchModel();
            var tags = db.Categories.Where(t => t.Name == tagName).Select(t => t).ToList();
            search.SearchResult = db.Tasks.Where(t => t.Categories.Where(tt => tt.Name == tagName).Select(tt => tt).Count() != 0).ToList();
            return search;
        }
        /// <summary>
        /// Find tasks in database using fulltext search
        /// </summary>
        /// <param name="searchModel">Search model wic represents the result</param>
        /// <returns></returns>
        public SearchModel SearchTasks(SearchModel searchModel)
        {
            String query = "select TaskId from [dbo].[Tasks] where contains(*, @param)";
            var searchReults = db.Database.SqlQuery<int>(query, new SqlParameter("param", "\"" + searchModel.Value + "*\"")).ToList();
            searchModel.SearchResult = new List<Task>();
            foreach (var searchReult in searchReults)
            {
                searchModel.SearchResult.Add(FindTask(searchReult));
            }
            return searchModel;
        }
        /// <summary>
        /// Executing an sql search query
        /// </summary>
        /// <param name="query">Text of the query</param>
        /// <param name="searchParameter">Text of search parameter</param>
        /// <returns></returns>
        public List<string> ExecuteSqlSearchQuery(string query, string searchParameter)
        {
            return db.Database.SqlQuery<string>(query, new SqlParameter("param", "\"" + searchParameter + "*\"")).ToList();
        }
        /// <summary>
        /// Check an answer
        /// </summary>
        /// <param name="taskId">Id of the task</param>
        /// <param name="answer">Id of the answer</param>
        /// <returns></returns>
        public bool IsAnswerCorrect(int taskId, string answer)
        {
            return db.Solutions.Where(t => t.TaskId == taskId).Select(t => t.Value).ToList().Contains(answer);
        }
        /// <summary>
        /// Dispose the entities model
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
        }
    }
}