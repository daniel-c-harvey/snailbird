using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SnailbirdAdminWeb.Client.API;
using SnailbirdAdminWeb.Client.Models;
using SnailbirdData.Models.Entities;
using SnailbirdMedia.Clients;
using SnailbirdMedia.Configs;

namespace SnailbirdAdminWeb.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            if (!Services.AddClientServices(builder.HostEnvironment.BaseAddress, builder.Services))
            {
                return; // Abort
            }

            await builder.Build().RunAsync();
        }
    }
}
