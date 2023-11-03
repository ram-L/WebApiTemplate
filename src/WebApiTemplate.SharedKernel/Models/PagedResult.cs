namespace WebApiTemplate.SharedKernel.Models
{
    /// <summary>
    /// Represents a paged result containing a collection of data items.
    /// </summary>
    /// <typeparam name="T">The type of data items in the paged result.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Gets or sets the collection of data items in the current page.
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the number of data items per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages in the paged result.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the total number of data items in the paged result.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets a value indicating whether there is a previous page.
        /// </summary>
        public bool HasPrevious => CurrentPage > 1;

        /// <summary>
        /// Gets a value indicating whether there is a next page.
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;
    }
}
