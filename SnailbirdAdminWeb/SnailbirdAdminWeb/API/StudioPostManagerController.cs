using Microsoft.AspNetCore.Mvc;
using SnailbirdAdminWeb.API.Managers;
using SnailbirdData.Models.Entities;

namespace SnailbirdAdminWeb.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudioPostManagerController : PostManagerController<StudioFeedFlexPost>
    {
        public StudioPostManagerController(IPostManager<StudioFeedFlexPost> manager) : base(manager) { }
    }
}
