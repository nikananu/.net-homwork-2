using Reddit.Models;
using Reddit.Requests;
using System.Linq.Expressions;

namespace Reddit.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly ApplicationDbContext _context;

        public PostsRepository(ApplicationDbContext applicationDBContext)
        {
            this._context = applicationDBContext;
        }

        public async Task<PagedList<Post>> GetAll(GetPostsRequest getPostsRequest)
        {
            IQueryable<Post> productsQuery = _context.Posts;

            if (!string.IsNullOrWhiteSpace(getPostsRequest.SearchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Title.Contains(getPostsRequest.SearchTerm) ||
                    p.Content.Contains(getPostsRequest.SearchTerm));
            }

            if (getPostsRequest.SortOrder?.ToLower() == "desc")
            {
                productsQuery = productsQuery.OrderByDescending(GetSortProperty(getPostsRequest.SortColumn));
            }
            else
            {
                productsQuery = productsQuery.OrderBy(GetSortProperty(getPostsRequest.SortColumn));
            }


            return await PagedList<Post>.CreateAsync(productsQuery, getPostsRequest.Page, getPostsRequest.PageSize);
        }

        private static Expression<Func<Post, object>> GetSortProperty(string SortColumn) =>
      SortColumn?.ToLower() switch
      {
          "date" => post => post.CreateAt,
          "popular" => post => post.Upvotes - post.Downvotes,
          _ => post => post.Id
      };
    }
}
