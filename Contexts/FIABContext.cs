using FIAB.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

		// Modern EF doesn't do validation, aparently.
		// This is our last safety net before writing bad data to the DB.
		// We must protecc the DB from (obviously) bad data.
		private IEnumerable<ValidationException> ValidateEntity(object entity)
		{
			var validationContext = new ValidationContext(entity);
			var results = new List<ValidationResult>();
			Validator.TryValidateObject(entity, validationContext, results, true);
			
			return results.Select(r => new ValidationException(r, null, entity));
		}

		private void ValidateBeforeSave()
		{
			var errors = new List<ValidationException>();
			var entries = ChangeTracker.Entries();
			var changes = entries.Where(t => t.State == EntityState.Modified || t.State == EntityState.Added || t.State == EntityState.Deleted);

			foreach (var change in changes)
			{
				errors.AddRange(ValidateEntity(change.Entity));
			}

			if(errors.Count > 0)
			{
				throw new AggregateException("Validation errors were encountered", errors);
			}
		}

		public override int SaveChanges()
		{
			ValidateBeforeSave();
			return base.SaveChanges();
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			ValidateBeforeSave();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			ValidateBeforeSave();
			return await base.SaveChangesAsync(cancellationToken);
		}

		public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			ValidateBeforeSave();
			return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		public DbSet<Noun> Nouns { get; set; }
		public DbSet<RelationshipType> RelationshipTypes { get; set; }
		public DbSet<Relationship> Relationships { get; set; }
	}
}