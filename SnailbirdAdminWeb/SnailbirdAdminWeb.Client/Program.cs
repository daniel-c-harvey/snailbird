using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

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
