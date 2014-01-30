using System.Collections.Generic;

namespace GenericProject.Core.Models
{
    public class PaginationRequest
    {
        // TODO: these filters still depend on magic strings....how to get a better paginationn request, that can work
        //       for every model, but also not then have 1 million defined column names...
        public IDictionary<string, string> FilterArgs { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string OrderBy { get; set; }

        internal int StartIndex { get { return PageSize * (PageNumber - 1); } }

        public PaginationRequest() { 
            PageSize = Constants.Paging.DeafultPageSize;
            PageNumber = Constants.Paging.FirstPage;
            FilterArgs = new Dictionary<string, string>(); 
        }

        public PaginationRequest(int pageSize = Constants.Paging.DeafultPageSize, int pageNumber = Constants.Paging.FirstPage, string orderBy = null)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            OrderBy = orderBy;
            FilterArgs = new Dictionary<string, string>();
        }

        public static PaginationRequest FirstPageOrderedBy(string orderBy) { return new PaginationRequest { OrderBy = orderBy }; }
    }

    public static class Constants
    {
        public static class Paging
        {
            public const int DeafultPageSize = 20;

            public const int FirstPage = 1;
        }
    }
}