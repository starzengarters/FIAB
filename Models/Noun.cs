using System.ComponentModel.DataAnnotations;

namespace FIAB.Models
{
	/// <summary>
	/// A thing which can related to other things.
	/// </summary>
	public class Noun : Entity
	{
		[Required(AllowEmptyStrings = false)]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// A brief summary for picking the correct Noun from a listing.
		/// </summary>
		public string? ShortDescription { get; set; }
		
		public string? Details { get; set; }
	}
}