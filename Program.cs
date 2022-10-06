using IsolaattiSoftwareWebsite;
using IsolaattiSoftwareWebsite.Services;
using Microsoft.AspNetCore.HttpOverrides;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

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
        config.ConnectionString =
            Environment.GetEnvironmentVariable("MONGO") ?? "mongodb://localhost:27017");

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

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();