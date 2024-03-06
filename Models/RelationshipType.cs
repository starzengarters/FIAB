using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIAB.Models
{
	/// <summary>
	/// Used with Relationship to determine what type of relationship exists between two things.
	/// </summary>
	public class RelationshipType : Entity
	{
		[Required(AllowEmptyStrings = false)]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// A brief summary for picking the correct RelationshipType from a listing.
		/// </summary>
		public string? ShortDescription { get; set; }

		/// <summary>
		/// The parent RelationshipType. Eg. "Sole Inventor" and "Co-Inventor" would probably both have a parent "Inventor"
		/// </summary>
		public RelationshipType? Parent { get; set; }

		[ForeignKey(nameof(Parent))]
		public int ParentId { get; set; }

		public string? Details { get; set; }
	}
}