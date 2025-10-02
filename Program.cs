using System.Runtime.Intrinsics.Arm;
using Blazored.Toast;
using Electionapp.UI.Components;
using Electionapp.UI.Services;
using Electionapp.UI.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add authentication and authorization
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//  Custom AuthenticationStateProvider (you must implement this)
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthenticationCore();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// These must be before app.MapBlazorHub()
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
