using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Fragmenter can control how text should be broken up in highlight snippets. However, this option is
	/// applicable only for the Plain Highlighter
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum HighlighterFragmenter
	{
		/// <summary>
		/// Breaks up text into same sized fragments.
		/// </summary>
		[EnumMember(Value = "simple")]
		Simple,
		/// <summary>
		/// Same as the simple fragmenter, but tries not to break up text between highlighted terms (this is applicable
		/// when using phrase like queries). This is the default.
		/// </summary>
		[EnumMember(Value = "span")]
		Span
	}
}
