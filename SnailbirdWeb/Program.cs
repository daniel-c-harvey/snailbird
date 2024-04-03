using Microsoft.AspNetCore.HttpOverrides;
using SnailbirdData.Providers;
using SnailbirdWeb.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddSingleton<IPostProvider, PostEmbeddedResourceProvider>();

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
