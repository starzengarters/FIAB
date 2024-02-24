using FIAB.Models;
using Microsoft.EntityFrameworkCore;

namespace FIAB.Contexts
{
	public class FIABContext : DbContext
	{
		public FIABContext(DbContextOptions<FIABContext> options) : base(options)
		{}

		public FIABContext()
		{
		}

		public static void UpdateOptions(DbContextOptionsBuilder options, string connectionString)
		{
			options
				.UseNpgsql(connectionString)
				.EnableDetailedErrors();
		}

		private static DbContextOptions<FIABContext> GetOptions(string connectionString)
		{
			var options = new DbContextOptionsBuilder<FIABContext>();

			options
				.UseNpgsql(connectionString)
				.EnableDetailedErrors();
			
			return options.Options;
		}

		public static FIABContext Create(string connectionString) => new FIABContext(GetOptions(connectionString));

		public static FIABContext Create(IConfiguration config)
		{
			var connectionString = Utilities.Env("ConnectionString", true, config);
			return Create(connectionString);
		}

		public DbSet<Noun> Nouns { get; set; }
		public DbSet<RelationshipType> RelationshipTypes { get; set; }
		public DbSet<Relationship> Relationships { get; set; }
	}
}