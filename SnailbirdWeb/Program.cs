using Microsoft.AspNetCore.HttpOverrides;
using SnailbirdData.Providers;
using SnailbirdWeb.Components;
using DataAccess;
using MongoDB.Driver;
using DataAccess;
using SnailbirdData.Models.Post;
using Microsoft.Extensions.DependencyInjection;

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

//MongoAdapterFactory postAdapterFacotry = new MongoAdapterFactory();
MongoAdapter<LiveJamPost> liveJamPostAdapter = new MongoAdapter<LiveJamPost>(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("studioLiveJamPost"));
MongoAdapter<FlexPost> studioFlexPostAdapter = new MongoAdapter<FlexPost>(dataResources.DataAccess, dataResources.QueryBuilder, new DataSchema("studioFeedFlexPost"));

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddSingleton<IDataAdapter<LiveJamPost>, MongoAdapter<LiveJamPost>>
    (provider => {
        return liveJamPostAdapter;
    })
    .AddSingleton<IPostProvider<LiveJamPost>, LiveJamPostMongoProvider>
    (provider => new LiveJamPostMongoProvider(liveJamPostAdapter))
    .AddSingleton<IDataAdapter<FlexPost>, MongoAdapter<FlexPost>>
    (provider => {
        return studioFlexPostAdapter;
    })
    .AddSingleton<IPostProvider<FlexPost>, FlexPostMongoProvider>
    (provider => new FlexPostMongoProvider(studioFlexPostAdapter));
    
//.AddSingleton<IPostProvider, PostEmbeddedResourceProvider>();

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
