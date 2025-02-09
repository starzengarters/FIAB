using System.ComponentModel.DataAnnotations;

namespace FIAB.Models
{

	public class RelationshipField : Entity
	{
		/// <summary>
		/// A short name to represent this field in a UI or other contexts.
		/// </summary>
		[Required(AllowEmptyStrings = false)]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// The the type of information we are hoping to store in this field.
		/// </summary>
		public string? Description { get; set; }
		
		public RelationFieldDataType DataType { get; set; } = RelationFieldDataType.unknown;
		
		[Required(AllowEmptyStrings = false)]
		public string Value { get; set; }  = string.Empty;
	}
}