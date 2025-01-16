using Microsoft.AspNetCore.Mvc;
using SnailbirdAdminWeb.API.Managers;
using SnailbirdData.Models.Entities;

namespace SnailbirdAdminWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabPostManagerController : PostManagerController<LabFeedFlexPost>
    {
        public LabPostManagerController(IPostManager<LabFeedFlexPost> manager) : base(manager) { }
    }
}
