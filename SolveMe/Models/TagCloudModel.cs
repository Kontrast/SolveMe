namespace SolveMe.Models
{
    /// <summary>
    /// Provides model for Tag Cloud (JQCloud)
    /// </summary>
    public class TagCloudModel
    {
        /// <summary>
        /// Gets or sets the tag name.
        /// </summary>
        /// <value>
        /// The tag name.
        /// </value>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the tag link for searching
        /// </summary>
        /// <value>
        /// The  link for searching
        /// </value>
        public string Link { get; set; }
        /// <summary>
        /// Gets or sets the weight of the tag.
        /// </summary>
        /// <value>
        /// The weight of the tag.
        /// </value>
        public string Weight { get; set; }
    }
}