using IsolaattiSoftwareWebsite;
using IsolaattiSoftwareWebsite.Model;
using IsolaattiSoftwareWebsite.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
        Environment.GetEnvironmentVariable("MONGO") ?? "mongodb://localhost:27017", "isolaattisoftware-db");

builder.Services.AddAuthorization().ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/SignIn";
});

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSendGrid(options =>
{
    options.ApiKey = builder.Environment.IsDevelopment()
        ? builder.Configuration["SendGrid:ApiKey"]
        : Environment.GetEnvironmentVariable("send_grid_api_key");
});
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<IFirstContactFormResponses, FirstContactFormResponsesService>();
builder.Services.AddScoped<IDeleteMyInformationService, DeleteMyInformationService>();

// For production the environment variable must be set
builder.Services.Configure<MongoConfiguration>(config =>
{
    config.ConnectionString =
        Environment.GetEnvironmentVariable("MONGO") ?? "mongodb://localhost:27017";
    config.DbName = "isolaattisoftware-db";
});


var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/error/{0}");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
}



app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    // Here check if there is no user. If so create a new one with default credentials
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var rolesManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

    if (!await rolesManager.RoleExistsAsync("admin"))
    {
        await rolesManager.CreateAsync(new ApplicationRole { Name = "admin" });
    }

    if (!userManager.Users.Any())
    {
        var defaultUser = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@isolaatti.com"
        };

        var userCreationResult = await userManager.CreateAsync(defaultUser, "98h87guKuyg-?");

        foreach (var error in userCreationResult.Errors)
        {
            Console.WriteLine(error.Description);
        }

        await userManager.AddToRoleAsync(defaultUser, "admin");
    }
}
app.Run();
