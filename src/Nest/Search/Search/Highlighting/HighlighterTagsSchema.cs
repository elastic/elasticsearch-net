using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Use a built-in tag schema
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum HighlighterTagsSchema
	{
		/// <summary>
		/// Use a specific "tag" schemas.
		/// </summary>
		/// <remarks>
		/// Currently a single schema called "styled" with the following pre_tags:
		/// &lt;em class="hlt1"&gt;, &lt;em class="hlt2"&gt;, &lt;em class="hlt3"&gt;,
		/// &lt;em class="hlt4"&gt;, &lt;em class="hlt5"&gt;, &lt;em class="hlt6"&gt;,
		/// &lt;em class="hlt7"&gt;, &lt;em class="hlt8"&gt;, &lt;em class="hlt9"&gt;,
		/// &lt;em class="hlt10"&gt;
		/// </remarks>
		[EnumMember(Value = "styled")]
		Styled
	}
}
