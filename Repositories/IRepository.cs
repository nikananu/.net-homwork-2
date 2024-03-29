using Reddit.Models;
using Reddit.Requests;

namespace Reddit.Repositories
{
    public interface IRepository<T>
    {
        public Task<PagedList<Post>> GetAll(GetPostsRequest getPostsRequest);
    }
}
