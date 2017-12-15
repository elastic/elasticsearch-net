using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Indicates if the highlighted text should be HTML encoded
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
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
