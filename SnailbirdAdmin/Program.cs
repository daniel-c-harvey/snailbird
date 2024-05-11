using SnailbirdAdmin.Components;
using DataAccess;
using SnailbirdData;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models;
using SnailbirdData.Providers;
using MongoDB.Driver;

namespace SnailbirdAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            AddGlobalServices(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }

        private static void AddGlobalServices(IServiceCollection services)
        {
            var dataAccess = new MongoDataAccess
            (
                Core.ConnectionStringTools.LoadFromFile("./.secrets/connections.json", "mongodb-snailbird-admin").ConnectionString,
                "snailbird-dev"
            );

            var queryBuilder = new MongoQueryBuilder();

            var dataResources = new DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder>
            (
                dataAccess,
                queryBuilder
            );

            MongoAdapter<LiveJamPost> postAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("posts"));

            services
            .AddSingleton<IDataAccess<IMongoDatabase>,MongoDataAccess>(provider => dataAccess)
            .AddSingleton<IDataAdapter<LiveJamPost>, MongoAdapter<LiveJamPost>>(provider => postAdapter)
            .AddSingleton<IPostProvider<LiveJamPost>, PostMongoProvider>(provider => new PostMongoProvider(postAdapter));

        }
    }
}
