namespace SolveMe.Models
{
    /// <summary>
    /// Represent the queries which are used to fulltext search
    /// </summary>
    public static class FulltextQueries
    {
        /// <summary>
        /// Query for searching titles of tasks
        /// </summary>
        public const string SelectTaskTitle = "select Title from [dbo].[Tasks] where contains(*, @param)";
        /// <summary>
        /// Query for searching text of tasks
        /// </summary>
        public const string SelectTaskText = "select TaskText from [dbo].[Tasks] where contains(*, @param)";
    }
}