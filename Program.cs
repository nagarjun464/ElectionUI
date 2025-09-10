    using System.Runtime.Intrinsics.Arm;
    using Electionapp.UI.Components;
    using Electionapp.UI.Services;

    var builder = WebApplication.CreateBuilder(args);

    
    builder.Services.AddHttpClient<ElectionApiClient>(c =>
    {
        c.BaseAddress = new Uri("https://electionapi-814747071660.us-central1.run.app/");
        Console.WriteLine("#####Client#######" + c.BaseAddress);
    });

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
