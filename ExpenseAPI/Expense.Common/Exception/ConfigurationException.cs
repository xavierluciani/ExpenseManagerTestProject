
namespace Expense.Common.Exception
{
    using System;

    /// <summary>
    /// Configuration exception at startup
    /// </summary>
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string message)
            : base($"Configuration exception: {message}")
        {
        }
    }
}
