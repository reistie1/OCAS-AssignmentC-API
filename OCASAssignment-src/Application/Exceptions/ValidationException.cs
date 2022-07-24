using FluentValidation.Results;

namespace OCASAPI.Application.Exceptions
{
    /// <summary>
    /// Custom validation exception inherits from <see cref="System.Exception"/>
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }
        
        /// <summary>
        /// Add error to list of errors
        /// </summary>
        /// <param name="failures"></param>
        /// <returns></returns>
        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}