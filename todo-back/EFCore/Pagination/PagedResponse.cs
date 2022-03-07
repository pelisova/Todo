using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Pagination
{
    public class PagedResponse<T> where T: class
    {
        public IEnumerable<T> Items { get; set; }
        public PaginationResult PaginationResult {get; set;}

        public PagedResponse(){} // required

        public PagedResponse(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            PaginationResult = new PaginationResult(count, pageNumber, pageSize);
        }
       

        public static async Task<PagedResponse<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResponse<T>(items, count, pageNumber, pageSize);
        }
    }
}