using Microsoft.EntityFrameworkCore;

namespace FIAB.Contexts
{
	public class FIABContext : DbContext
	{
		public FIABContext(DbContextOptions<FIABContext> options) : base(options)
		{}

		public static FIABContext Create(string connectionString)
		{
			var options = new DbContextOptionsBuilder<FIABContext>();

			options
				.UseNpgsql(connectionString)
				.EnableDetailedErrors();

			return new FIABContext(options.Options);
		}

		public static FIABContext Create(IConfiguration config)
		{
			var connectionString = Models.Utilities.Env("ConnectionString", true, config);
			return Create(connectionString);
		}
	}
}