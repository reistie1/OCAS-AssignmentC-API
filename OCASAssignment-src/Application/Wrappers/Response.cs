
namespace OCASAPI.Application.Wrappers
{
    /// <summary>
    /// Wrapper class for basic responses, returns request success status, message, data and a list of errors
    /// that occurred while processing the request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T>
    {
        public Response(){}
        
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}