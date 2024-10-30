namespace UserManagerService.Models.Dtos
{
    public class PagenatedItemDto<TEntity> where TEntity : class
    {
        public PagenatedItemDto(int pageIndex, int pageSize, int count, IEnumerable<TEntity> entities)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Items = entities;
            Pager = new Pager(count, PageIndex, PageSize);

        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }


        public IEnumerable<TEntity> Items { get; set; }

        public Pager Pager { get; set; }

        public bool HasPrevPage
        {
            get
            {
                return PageIndex > 1;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < Pager.TotalPages);
            }
        }
    }

    public class Pager
    {
        public Pager(int totalItems, int currentPage = 1, int pageSize = 10, int maxPages = 5)
        {
            // calculate total pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

            // ensure current page isn't out of range
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            int startPage, endPage;
            if (totalPages <= maxPages)
            {
                // total pages less than max so show all pages
                startPage = 1;
                endPage = totalPages;
            }
            else
            {
                // total pages more than max so calculate start and end pages
                var maxPagesBeforeCurrentPage = (int)Math.Floor((decimal)maxPages / (decimal)2);
                var maxPagesAfterCurrentPage = (int)Math.Ceiling((decimal)maxPages / (decimal)2) - 1;
                if (currentPage <= maxPagesBeforeCurrentPage)
                {
                    // current page near the start
                    startPage = 1;
                    endPage = maxPages;
                }
                else if (currentPage + maxPagesAfterCurrentPage >= totalPages)
                {
                    // current page near the end
                    startPage = totalPages - maxPages + 1;
                    endPage = totalPages;
                }
                else
                {
                    // current page somewhere in the middle
                    startPage = currentPage - maxPagesBeforeCurrentPage;
                    endPage = currentPage + maxPagesAfterCurrentPage;
                }
            }
            // calculate start and end item indexes
            var startIndex = (currentPage - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize - 1, totalItems - 1);

            // create an array of pages that can be looped over
            var pages = Enumerable.Range(startPage, (endPage + 1) - startPage);


            TotalPages = totalPages;
            Pages = pages;
        }
        public IEnumerable<int> Pages { get; private set; }
        public int TotalPages { get; set; }

    }
}
