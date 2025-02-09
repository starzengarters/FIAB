namespace FIAB.Models
{
	public enum RelationFieldDataType
	{
		// These are fields we want to know, but do not have answers for, yet.
		unknown,
		// This will result in a Duration - a start and end time since we often have approximate dates.
		// Should have a parser that can take strings like ~1894, 1894-1902, August 1921 etc. and provide suitable start/end dates.
		// Optionally the user should be able to easily set a custom range.
		// This could be neat for time series information.
		Date,
		// Very generic, could be a name, could be the full text of a pattent.
		Text,
		// Another fuzzy input, like date, but works out to a double.
		// Again tries to parse all sorts of values but must evaluate to a double.
		Number,
		// Commit to the bit.
		Boolean,
		// WBN: let folks do markdown formatting
		// Markdown
		//WBN: if we integrate file management, link to primary sources.
		// Document
	}
}