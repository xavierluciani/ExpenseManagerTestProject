
namespace Expense.Common.Exception
{
    using System;

    /// <summary>
    /// Web exception to manage controller returns
    /// </summary>
    public abstract class WebException : Exception
    {
        protected WebException(string message, string httpCode)
            : base($"Web exception: [{httpCode}] {message}")
        {
        }
    }
}
