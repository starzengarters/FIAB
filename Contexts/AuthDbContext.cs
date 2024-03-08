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
	}
}