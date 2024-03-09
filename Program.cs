using FIAB.Contexts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Components.Authorization;
using FIAB.Providers;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorBootstrap();
builder.Services.AddDbContext<FIABContext>(options =>
{
	var cs = FIAB.Models.Utilities.Env("ConnectionString", true, builder.Configuration);
	FIABContext.UpdateOptions(options, cs);
});

// Setting up authentication
// if credentials were supplied add an EmailSender for registration and password recovery.
var s = FIAB.Models.Utilities.Env("SmtpServer", false, builder.Configuration, string.Empty);
var pt = FIAB.Models.Utilities.Env("SmtpPort", true, builder.Configuration);
var u = FIAB.Models.Utilities.Env("SmtpUser", false, builder.Configuration, string.Empty);
var p = FIAB.Models.Utilities.Env("SmptPassword", false, builder.Configuration, string.Empty);
if(!string.IsNullOrWhiteSpace(s) && !string.IsNullOrWhiteSpace(pt) && !string.IsNullOrWhiteSpace(u) && !string.IsNullOrWhiteSpace(p))
{
	builder.Services.AddTransient<IEmailSender, EmailSender>();
}

// Configure injection for AuthDbContext.
builder.Services.AddDbContext<AuthDbContext>(options =>
{
	var cs = FIAB.Models.Utilities.Env("ConnectionString", true, builder.Configuration);
	AuthDbContext.UpdateOptions(options, cs);
});

// Settings for Identity/Login
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AuthDbContext>();

// Inject AuthenticationStateProvider for checking authentication status.
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

// End of authentication.

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

/* Migrate DB to latest */
try
{
	Console.WriteLine($"Creating database context for migration...");
	using (var context = FIABContext.Create(builder.Configuration))
	{
		Console.WriteLine($"\tMigrating database...");
		context.Database.Migrate();
		Console.Error.WriteLine($"\tMigrated database.");
	}
}
catch (Exception ex)
{
	Console.Error.WriteLine($"Error migrating database: {ex.Message}");
	throw new Exception($"Error migrating database.", ex);
}
/* End of Migrate */

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
