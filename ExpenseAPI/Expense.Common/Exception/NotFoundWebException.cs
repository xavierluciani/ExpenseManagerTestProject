
namespace Expense.Common.Exception
{
    using System.Net;

    /// <summary>
    /// Not found 404 exception
    /// </summary>
    public class NotFoundWebException : WebException
    {
        public NotFoundWebException(string message)
            :base(message, HttpStatusCode.NotFound.ToString())
        {
        }
    }
}
