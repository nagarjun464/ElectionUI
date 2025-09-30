using System.Runtime.Intrinsics.Arm;
using Blazored.Toast;
using Electionapp.UI.Components;
using Electionapp.UI.Services;
using Electionapp.UI.Models;

var builder = WebApplication.CreateBuilder(args);

    
builder.Services.AddHttpClient<ElectionApiClient>(c =>
{
    c.BaseAddress = new Uri("https://electionapi-814747071660.us-central1.run.app/");
    Console.WriteLine("#####Client#######" + c.BaseAddress);
});
builder.Services.AddHttpClient<PresidentCountyApiClient>(c =>
{
    c.BaseAddress = new Uri("https://president-county-api-814747071660.us-central1.run.app");
    Console.WriteLine("#####PresidentCountyClient#######" + c.BaseAddress);
});
    
builder.Services.AddHttpClient<SignupAPI>(c  => 
{
    c.BaseAddress = new Uri("https://signup-api-814747071660.us-central1.run.app");
    Console.WriteLine("#####SignupAPI#######" + c.BaseAddress);
});

builder.Services.AddHttpClient<LoginAPI>(c =>
{
    c.BaseAddress = new Uri("https://signup-api-814747071660.us-central1.run.app");
    Console.WriteLine("#####LoginAPI#######" + c.BaseAddress);
});

builder.Services.AddBlazoredToast();
builder.Services.AddSingleton<LoginState>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
