using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballPlayers.Models
{
    public class PageList<T>
    {
        public PageList(IQueryable<T> items, int currentPage, int pageSize)
        {
            if (currentPage < 1) currentPage = 1;
            if (pageSize < 1) pageSize = 100;
            Items = items.Skip(pageSize*(currentPage-1)).Take(pageSize).ToList();
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = items.Count();
            TotalPagesCount = (int)Math.Ceiling(TotalCount / (double)pageSize);
        }
        public int PageSize { get; }
        public long TotalCount { get; }
        public int TotalPagesCount { get; }
        public int CurrentPage { get; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPagesCount;
        public List<T> Items { get; }
    }
}
