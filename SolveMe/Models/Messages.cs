namespace SolveMe.Models
{
    /// <summary>
    /// Represent messages witch can be sended to user
    /// </summary>
    public static class Messages
    {
        public const string UserSuccess = "Corect answer! You'v got +50 points to rating.";
        public const string UserFail = "Fail! You'v lost 5 rating points.";
        public const string UserAlreadySolve = "Sorry, you'v already solve this task!";
        public const string UserAuthor = "You are author of this task, so you cant solve it!";
        public const string TaskErrorTitle = "Task isn't exists!";
        public const string TaskErrorMessage = "May be this task was deleted.";
    }
}