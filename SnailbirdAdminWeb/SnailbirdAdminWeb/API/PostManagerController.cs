using Microsoft.AspNetCore.Mvc;
using NetBlocks.Models;
using SnailbirdAdminWeb.API.Managers;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.API
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
        public ResultContainer<IEnumerable<TPost>> GetPage([FromQuery] int page, [FromQuery] int size)
        {
            if (page < 0 || size <= 0) 
            { 
                return ResultContainer<IEnumerable<TPost>>.CreateFailResult(""); 
            }

            return manager.GetPosts(page, size);
        }

        [HttpPost("save")]
        public Result Update([FromBody] TPost post)
        {
            return manager.SavePost(post);
        }
        
        [HttpPost("insert")]
        public Result Insert([FromBody] TPost post)
        {
            return manager.InsertPost(post);
        }

        [HttpPost("delete")]
        public Result Delete([FromBody] TPost post)
        {
            return manager.DeletePost(post);
        }



        //// GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
