using System.Collections.Generic;
using System.Web.Mvc;

namespace SolveMe.Models
{
    /// <summary>
    /// Represens the results of search
    /// </summary>
    public class SearchModel
    {
        /// <summary>
        /// Gets or sets a search string
        /// </summary>
        /// <value>
        /// Search string
        /// </value>
        [AllowHtml]
        public string Value { get; set; }
        /// <summary>
        /// Gets or sets list of search results
        /// </summary>
        /// <value>
        /// List of search results
        /// </value>
        public List<Task> SearchResult { set; get; }
    }
}