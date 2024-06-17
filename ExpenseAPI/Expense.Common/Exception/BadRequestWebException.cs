
namespace Expense.Common.Exception
{
    using System.Net;

    /// <summary>
    /// Bad request 400 exception
    /// </summary>
    public class BadRequestWebException : WebException
    {
        public BadRequestWebException(string message)
            : base(message, HttpStatusCode.BadRequest.ToString())
        {
        }
    }
}
