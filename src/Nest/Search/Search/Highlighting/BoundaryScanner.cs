using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum BoundaryScanner
	{
		/// <summary>
		/// (default mode for the FVH): allows to configure which characters (boundary_chars) constitute a boundary for highlighting. It’s a single
		/// string with each boundary character defined in it (defaults to .,!? \t\n). It also allows configuring the boundary_max_scan to
		/// control how far to look for boundary characters (defaults to 20). Works only with the Fast Vector Highlighter.
		/// </summary>
		[EnumMember(Value = "chars")]
		Characters,
		/// <summary>
		/// sentence and word: use Java’s BreakIterator to break the highlighted fragments at the next sentence or word boundary.
		/// You can further specify boundary_scanner_locale to control which Locale is used to search the text for these boundaries.
		/// </summary>
		[EnumMember(Value = "sentence")]
		Sentence,
		/// <summary>
		/// sentence and word: use Java’s BreakIterator to break the highlighted fragments at the next sentence or word boundary.
		/// You can further specify boundary_scanner_locale to control which Locale is used to search the text for these boundaries.
		/// </summary>
		[EnumMember(Value = "word")]
		Word
	}
}
