using Reddit.Models;
using Reddit.Requests;
using System.Linq.Expressions;

namespace Reddit.Repositories
{
    public class CommunityRepository : ICommunityRepository
    {
        private readonly ApplicationDbContext _context;

        public CommunityRepository(ApplicationDbContext applicationDBContext)
        {
            this._context = applicationDBContext;
        }
        public async Task<PagedList<Community>> GetAll(GetCommunityRequest getCommunityRequest)
        {
            
                IQueryable<Community> productsQuery = _context.Communities;

                if (!string.IsNullOrWhiteSpace(getCommunityRequest.SearchTerm))
                {
                    productsQuery = productsQuery.Where(p =>
                       p.Name.Contains(getCommunityRequest.SearchTerm) ||
                        p.Description.Contains(getCommunityRequest.SearchTerm));
                }

            if (getCommunityRequest.SortOrder?.ToLower() == "desc")
            {
                productsQuery = productsQuery.OrderByDescending(GetSortProperty(getCommunityRequest.SortColumn));
            }
            else
            {
                productsQuery = productsQuery.OrderBy(GetSortProperty(getCommunityRequest.SortColumn));
            }


            return await PagedList<Community>.CreateAsync(productsQuery, getCommunityRequest.Page, getCommunityRequest.PageSize);
        
    
        }


        private static Expression<Func<Community, object>> GetSortProperty(string SortColumn) =>
      SortColumn?.ToLower() switch
      {
          "date" => Community => Community.CreateAt,
          "postcount" => Community => Community.PostsCount,
          "subscribecount" => Community => Community.SubscribersCount,
          _ => Community => Community.Id
      };
    }
}
