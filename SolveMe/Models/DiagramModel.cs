using System;
using System.Collections.Generic;

namespace SolveMe.Models
{
    /// <summary>
    /// Represents the data for diagam of user activity
    /// </summary>
    public class DiagramModel
    {
        /// <summary>
        /// Gets or sets name of a diagram
        /// </summary>
        /// <value>
        /// Name of the diagram
        /// </value>
        public String name { get; set; }
        /// <summary>
        /// Gets or sets data to display on the diagram
        /// </summary>
        /// <value>
        /// Data to display on the diagram
        /// </value>
        public List<List<object>> data { get; set; }
    }
}