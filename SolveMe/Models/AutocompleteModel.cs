using System.Collections.Generic;

namespace SolveMe.Models
{
    /// <summary>
    /// Autocompition model for search
    /// </summary>
    public class AutocompleteModel
    {
        /// <summary>
        /// Gets or sets the search query
        /// </summary>
        /// <value>
        /// Search query string
        /// </value>
        public string query { get; set; }
        /// <summary>
        /// Gets or sets matches founded in database
        /// </summary>
        /// <value>
        /// Mathes list
        /// </value>
        public List<string> suggestions { get; set; }
        /// <summary>
        /// Gets or sets optional data to display in autocomplit field
        /// </summary>
        /// <value>
        /// Optional data list
        /// </value>
        public List<string> data { get; set; } 
    }
}