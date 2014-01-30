using System;
using System.Collections;
using System.Collections.Generic;
using GenericProject.Core.Models;

namespace GenericProject.Core.Model
{
    public class PagedEnumerable<T>
    {
        public readonly IEnumerable<T> Items;

        public PaginationRequest PaginationRequest { get; private set; }

        public int PageCount { get; private set; }

        public int TotalCount { get; private set; }

        public int PageSize { get; private set; }

        public PagedEnumerable(IEnumerable<T> items, PaginationRequest paginationRequest, int totalCount)
        {
            Items = items;
            PaginationRequest = paginationRequest;
            PageCount = GetPageCount(PaginationRequest, totalCount);
            TotalCount = totalCount;
            PageSize = PaginationRequest.PageSize;
        }

        private int GetPageCount(PaginationRequest paginationRequest, int totalCount)
        {
            if (paginationRequest == null || paginationRequest.PageSize == 0)
                return totalCount;

            return (int)Math.Ceiling((double)totalCount / paginationRequest.PageSize);
        }

    }
}