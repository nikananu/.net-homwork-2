using Reddit.Models;
using Reddit.Requests;

namespace Reddit.Repositories
{
    public interface Irepositorycommunity<T>
    {
        public Task<PagedList<Community>> GetAll(GetCommunityRequest getCommunityRequest);
    }
}
