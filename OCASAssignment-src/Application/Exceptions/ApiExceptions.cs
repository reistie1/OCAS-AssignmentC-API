using System.Globalization;

namespace OCASAPI.Application.Exceptions
{
    /// <summary>
    /// Custom API exception inherits from <see cref="System.Exception"/>
    /// </summary>
    public class ApiExceptions : Exception
    {
        public ApiExceptions() : base() { }

        public ApiExceptions(string message) : base(message) { }

        public ApiExceptions(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}