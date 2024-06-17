
namespace Expense.Common.Exception
{
    using System.Net;

    /// <summary>
    /// Conflict 409 exception
    /// </summary>
    public class ConflictWebException : WebException
    {
        public ConflictWebException(string message)
            : base(message, HttpStatusCode.Conflict.ToString())
        {
        }
    }
}
