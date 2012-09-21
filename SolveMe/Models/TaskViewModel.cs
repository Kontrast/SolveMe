using System.Web.Mvc;

namespace SolveMe.Models
{
    /// <summary>
    /// Represents task create view
    /// </summary>
    public class TaskViewModel
    {
        /// <summary>
        /// Gets or sets title of a task
        /// </summary>
        /// <value>
        /// Title of the task
        /// </value>
        [AllowHtml]
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets text of a task
        /// </summary>
        /// <value>
        /// Text of the task
        /// </value>
        [AllowHtml]
        public string TaskText { get; set; }
        /// <summary>
        /// Gets or sets json string of categories
        /// </summary>
        /// <value>
        /// Json string of categories
        /// </value>
        [AllowHtml]
        public string Categories { get; set; }
        /// <summary>
        /// Gets or sets json string of answers
        /// </summary>
        /// <value>
        /// Json string of answers
        /// </value>
        [AllowHtml]
        public string Answers { get; set; }
    }
}