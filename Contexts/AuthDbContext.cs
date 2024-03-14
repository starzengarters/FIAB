using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FIAB.Contexts
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options)
			: base(options)
		{
		}

		public static void UpdateOptions(DbContextOptionsBuilder options, string connectionString)
		{
			options
				.UseNpgsql(connectionString, x => x.MigrationsHistoryTable("__AuthMigrationHistory"))
				.EnableDetailedErrors();
		}

		public static AuthDbContext Create(string connectionString)
		{
			var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
			UpdateOptions(optionsBuilder, connectionString);
			
			return new AuthDbContext(optionsBuilder.Options);
		}

		public static AuthDbContext Create(IConfiguration config)
		{
			var connectionString = Models.Utilities.Env("ConnectionString", true, config);
			return Create(connectionString);
		}

		// seed default roles.
		// Thanks, Chris Sainty! https://chrissainty.com/securing-your-blazor-apps-configuring-role-based-authorization-with-client-side-blazor/
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
			builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
		}
	}
}