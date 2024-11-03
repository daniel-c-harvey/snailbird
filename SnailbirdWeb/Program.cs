using Microsoft.AspNetCore.HttpOverrides;
using SnailbirdData.Providers;
using SnailbirdWeb.Components;
using DataAccess;
using MongoDB.Driver;
using DataAccess;
using SnailbirdData.Models.Post;
using Microsoft.Extensions.DependencyInjection;
using SnailbirdData.Models.Entities;

var builder = WebApplication.CreateBuilder(args);



var dataResources = new DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder>
    (
        new MongoDataAccess(
            Core.ConnectionStringTools.LoadFromFile("./.secrets/connections.json", "mongodb-snailbird")
                .ConnectionString,
                "snailbird-dev"
            ),
        new MongoQueryBuilder()
);

MongoAdapter<LiveJamPost> liveJamPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("studioLiveJamPost"));
MongoAdapter<StudioFeedFlexPost> studioFlexPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("studioFeedFlexPost"));
MongoAdapter<LabFeedFlexPost> labFlexPostAdapter = new(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("studioFeedFlexPost"));

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddSingleton<IDataAdapter<LiveJamPost>, MongoAdapter<LiveJamPost>>
    (_ => liveJamPostAdapter)
    .AddSingleton<IDataAdapter<StudioFeedFlexPost>, MongoAdapter<StudioFeedFlexPost>>
    (_ => studioFlexPostAdapter)
    .AddSingleton<IDataAdapter<LabFeedFlexPost>, MongoAdapter<LabFeedFlexPost>>
    (_ => labFlexPostAdapter);
    

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseAntiforgery();

// if environment is nginx configure the reverse proxy middleware
if (app.Environment.IsEnvironment("nginx")) 
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
                            {
                                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                            });
}
else // Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
