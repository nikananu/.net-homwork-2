using Microsoft.AspNetCore.Mvc;

namespace Reddit.Requests
{
    public class GetCommunityRequest
    {
       
        
            [FromQuery]
            public string? SearchTerm { get; set; }

            [FromQuery]
            public string? SortColumn { get; set; }

            [FromQuery]
            public string? SortOrder { get; set; }

            [FromQuery]
            public int Page { get; set; } = 1;

            [FromQuery]
            public int PageSize { get; set; } = 10;
        
    }
}
