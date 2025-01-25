using Microsoft.AspNetCore.Mvc;
using NetBlocks.Models;
using SnailbirdAdminWeb.API.Managers;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.API.Controllers
{
    [ApiController]
    public class PostManagerController<TPost> : ControllerBase
        where TPost : Post<TPost>
    {
        private IPostManager<TPost> manager { get; }

        public PostManagerController(IPostManager<TPost> manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public async Task<ResultContainer<IEnumerable<TPost>>.ResultContainerDto<IEnumerable<TPost>>> 
            GetPage([FromQuery] int page, [FromQuery] int size)
        {
            if (page < 0 || size <= 0) 
            { 
                return new ResultContainer<IEnumerable<TPost>>.ResultContainerDto<IEnumerable<TPost>>(
                    ResultContainer<IEnumerable<TPost>>.CreateFailResult(
                        "Invalid page arguments; page must be greater than or equal to 0; size must be greater than 0.")); 
            }

            return new (await manager.GetPosts(page, size));
        }

        [HttpPost("save")]
        public async Task<Result.ResultDto> Update([FromBody] TPost post)
        {
            return new Result.ResultDto(await manager.SavePost(post));
        }
        
        [HttpPost("insert")]
        public async Task<Result.ResultDto> Insert([FromBody] TPost post)
        {
            return new Result.ResultDto(await manager.InsertPost(post));
        }

        [HttpPost("delete")]
        public async Task<Result.ResultDto> Delete([FromBody] TPost post)
        {
            return new Result.ResultDto(await manager.DeletePost(post));
        }
    }
}
