using FIAB.Contexts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<FIABContext>(options =>
{
	var cs = FIAB.Models.Utilities.Env("ConnectionString", true, builder.Configuration);
	FIABContext.UpdateOptions(options, cs);
});

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
