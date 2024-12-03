using Microsoft.AspNetCore.HttpOverrides;
using SnailbirdWeb.Components;
using DataAccess;
using MongoDB.Driver;
using SnailbirdData.Models.Post;
using SnailbirdData.Models.Entities;
using NetBlocks.Models.Environment;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);
Connections? connectionSecrets = builder.Configuration.Get<Connections>();

if (connectionSecrets == null)
{
    Console.WriteLine("Unable to load connection secrets");
    return;
}

Connection? connection = connectionSecrets.ConnectionStrings?
                         .FirstOrDefault(c => c.ID == connectionSecrets.ActiveConnectionID);

if (connection == null)
{
    Console.WriteLine("Active connection does not point to a valid connection string");
    return;
}

DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder> dataResources;
try
{ 
    dataResources = new DataResources<IMongoDatabase, MongoDataAccess, MongoQueryBuilder>
    (
        new MongoDataAccess(connection.ConnectionString, connection.DatabaseName),
        new MongoQueryBuilder()
    );
}
catch (Exception e)
{
    Console.WriteLine($"Error encountered while initializing database connection:\r\n{e.Message}");
    return;
}

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
