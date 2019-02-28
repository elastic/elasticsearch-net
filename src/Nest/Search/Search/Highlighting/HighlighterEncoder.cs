using System.Runtime.Serialization;
using Elasticsearch.Net;


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
