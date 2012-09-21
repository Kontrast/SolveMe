namespace SolveMe.Models
{
    /// <summary>
    /// Represents answer information
    /// </summary>
    public class AnswerInfo
    {
        /// <summary>
        /// Gets or sets text of the answer
        /// </summary>
        /// <value>
        /// The answer string
        /// </value>
        public string Answer { set; get; }
        /// <summary>
        /// Gets or sets id of user which gives the answer
        /// </summary>
        /// <value>
        /// The id of user
        /// </value>
        public int UserId { set; get; }
        /// <summary>
        /// Gets or sets id of task which is solving
        /// </summary>
        /// <value>
        /// The id of task
        /// </value>
        public int TaskId { set; get; }
    }
}