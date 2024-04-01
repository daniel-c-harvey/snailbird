using Microsoft.AspNetCore.HttpOverrides;
using SnailbirdWeb.Components;

var builder = WebApplication.CreateBuilder(args);

// part of the revproxy
// builder.Services.AddAuthentication();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// app.UseForwardedHeaders(new ForwardedHeadersOptions
// {
//     ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
// });

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// app.UseAuthentication();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// app.MapGet("/", () => "Hello ForwardedHeadersOptions!");

app.Run();
