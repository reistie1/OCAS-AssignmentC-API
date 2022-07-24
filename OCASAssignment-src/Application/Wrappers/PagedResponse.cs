
namespace OCASAPI.Application.Wrappers
{
    /// <summary>
    /// response wrapper class used to return list of given entity regarding the page number, 
    /// size, total records, data and other data related to the the request as well as any errors that 
    /// happened while processing the request.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages {get; set;}

        public PagedResponse(T data, int pageNumber, int pageSize, int TotalPages)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalPages = TotalPages;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}