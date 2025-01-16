using Microsoft.AspNetCore.Mvc;
using NetBlocks.Models;
using NetBlocks.Models.Environment;
using SnailbirdAdminWeb.API.Managers;

namespace SnailbirdAdminWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
        private IConnectionManager manager { get; }

        public ConnectionsController(IConnectionManager manager)
        {
            this.manager = manager;   
        }

        [HttpGet]
        public ResultContainer<Connections> GetConnections()
        {
            return manager.GetConnections();
        }

        [HttpPost]
        public Result SaveConnections([FromBody] Connections connections)
        {
            return manager.SaveConnections(connections);
        }
    }
}
