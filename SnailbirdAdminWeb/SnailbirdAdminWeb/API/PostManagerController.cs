﻿using Microsoft.AspNetCore.Mvc;
using NetBlocks.Models;
using SnailbirdAdminWeb.API.Managers;
using SnailbirdData.Models.Post;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            if (page < 1 || size <= 0) 
            { 
                return ResultContainer<IEnumerable<TPost>>.CreateFailResult(""); 
            }

            return manager.GetPosts(page - 1, size);
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
