using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum HighlighterType
	{
		[EnumMember(Value = "plain")]
		Plain,
		[EnumMember(Value = "postings")]
		Postings,
		[EnumMember(Value = "fvh")]
		Fvh
	}
}
