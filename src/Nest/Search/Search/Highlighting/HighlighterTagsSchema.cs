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
		/// <para>Currently a single schema called "styled" with the following pre_tags:</para>
		/// <para>&lt;em class="hlt1"&gt;, &lt;em class="hlt2"&gt;, &lt;em class="hlt3"&gt;,</para>
		/// <para>&lt;em class="hlt4"&gt;, &lt;em class="hlt5"&gt;, &lt;em class="hlt6"&gt;,</para>
		/// <para>&lt;em class="hlt7"&gt;, &lt;em class="hlt8"&gt;, &lt;em class="hlt9"&gt;,</para>
		/// <para>&lt;em class="hlt10"&gt;</para>
		/// </remarks>
		[EnumMember(Value = "styled")]
		Styled
	}
}
