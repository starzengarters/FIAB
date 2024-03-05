using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIAB.Models
{
	/// <summary>
	/// A relationship between two Nouns.
	/// Eg. Noun "Smith & Wesson" RelationshipType "owned" Noun "US Patent 12648", Started (approximately) "1855-01-01".
	/// </summary>
	public class Relationship : Entity
	{
		[Required]
		public Noun? Subject { get; set; }

		[Required]
		public RelationshipType? RelationshipType { get; set; }

		[ForeignKey(nameof(RelationshipType))]
		public int RelationshipTypeId { get; set; }

		[Required]
		public Noun? Object { get; set; }

		[ForeignKey(nameof(Object))]
		public int ObjectId { get; set; }

		public DateOnly? Started { get; set; }
		public bool StartedIsApproximate { get; set; } = false;
		public DateOnly? Ended { get; set; }
		public bool EndedIsApproximate { get; set; } = false;

		// TODO - Add citations list here.
		// TODO allow to "retire" a relationship in 2 ways
		// 1.) A "refutation" where you detail why it was delted.
		// 2.) A "I was just wrong/Bad data entry"
		// Maybe it should allow a true deletion, too.

		/// <summary>
		/// Copies the contents of this Relationship into the provied input.
		/// </summary>
		/// <param name="input">The relationship to update the value of.</param>
		public void DeepCopy(Relationship input)
		{
			input.Id = this.Id;
			input.Subject = this.Subject;
			input.RelationshipType = this.RelationshipType;
			input.RelationshipTypeId = this.RelationshipTypeId;
			input.Object = this.Object;
			input.ObjectId = this.ObjectId;
			input.Started = this.Started;
			input.StartedIsApproximate = this.StartedIsApproximate;
			input.Ended = this.Ended;
			input.EndedIsApproximate = this.EndedIsApproximate;
		}
	}
}