using System.Runtime.Serialization;


namespace Nest
{
	/// <summary>
	/// Indicates if the highlighted text should be HTML encoded
	/// </summary>
	[StringEnum]
	public enum HighlighterEncoder
	{
		/// <summary>
		/// No encoding
		/// </summary>
		[EnumMember(Value = "default")]
		Default,

		/// <summary>
		/// Escapes HTML highlighting tags
		/// </summary>
		[EnumMember(Value = "html")]
		Html
	}
}
