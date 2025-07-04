using Blog.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Blog.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register blog services
builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<MarkdownService>();

await builder.Build().RunAsync();
