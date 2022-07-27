namespace OCASAPI.Application.Parameters
{
    public class RequestParameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search {get; set; } = null;
        public RequestParameters()
        {
            this.PageNumber = 1;
            this.PageSize = 50;
            this.Search = null;
        }
        public RequestParameters(int pageNumber, int pageSize, string search)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 50 ? 50 : pageSize;
            this.Search = search;
        }
    }
}