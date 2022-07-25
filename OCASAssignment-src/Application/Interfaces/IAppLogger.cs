namespace OCASAPI.Application.Interfaces
{
    /// <summary>
    /// Generic logger interface of methods to log information and warnings that occur during the execution
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAppLogger<T>
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
    }
}