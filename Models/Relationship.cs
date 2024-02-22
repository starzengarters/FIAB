using System.ComponentModel.DataAnnotations;

namespace FIAB.Models
{
	/// <summary>
	/// A relationship between two Nouns.
	/// Eg. Noun "Smith & Wesson" RelationshipType "owned" Noun "US Patent 12648", Started (approximately) "1855-01-01".
	/// </summary>
	public class Relationship : Entity
	{
		[Required]
		public required Noun NounA { get; set; }

		[Required]
		public required Noun NounB { get; set; }

		[Required]
		public required RelationshipType RelationshipType { get; set; }

		public DateOnly? Started { get; set; }
		public bool StartedIsApproximate { get; set; } = false;
		public DateOnly? Ended { get; set; }
		public bool EndedIsApproximate { get; set; } = false;
	}
}