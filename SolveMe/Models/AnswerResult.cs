namespace SolveMe.Models
{
    /// <summary>
    /// Represents the result of solving attempt
    /// </summary>
    public class AnswerResult
    {
        /// <summary>
        /// Gets or sets is attempt success
        /// </summary>
        /// <value>
        /// Success bool
        /// </value>
        public bool Success { get; set; }
        /// <summary>
        /// Gets or sets message which will send back to user
        /// </summary>
        /// <value>
        /// Message string
        /// </value>
        public string Message { get; set; }
    }
}