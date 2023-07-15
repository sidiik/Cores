using System.Linq.Dynamic.Core;
using System.Reflection;

namespace SimpleProject.Data
{
    public class ApiResult<T>
    {
        private ApiResult(List<T> data, int count, int pageIndex, int pageSize, string? sortColumn, string? sortOrder)
        {
            Data = data;
            TotalItems = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize) - 1;
            PageSize = pageSize;
            CurrentPage = pageIndex;

        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public bool HasPreviousPage => CurrentPage > 0;
        public bool HasNextPage => CurrentPage + 1 < TotalPages;

        public List<T>? Data { get; set; }

        private static bool IsValidProperty(string propertyName, bool throwNotFoundException = true)
        {
            var prop = typeof(T).GetProperty(
                propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public
            );

            if (prop == null && throwNotFoundException) throw new NotSupportedException($"Property {propertyName} is not supported");

            return prop != null;
        }
        public static async Task<ApiResult<T>> CreateAsync(
        IQueryable<T> source,
         int pageIndex,
         int pageSize,
         string? sortColumn = null,
         string? sortOrder = null
         )
        {
            var count = await source.CountAsync();
            if (!string.IsNullOrEmpty(sortColumn) && IsValidProperty(sortColumn))
            {
                sortOrder = !string.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper() == "ASC" ? "ASC" : "DESC";
                source = source.OrderBy($"{sortColumn} {sortOrder}");
            }
            var result = await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

            return new ApiResult<T>(result, count, pageIndex, pageSize, sortColumn, sortOrder);

        }

    }
}