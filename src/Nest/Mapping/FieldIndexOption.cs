using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FieldIndexOption
	{
		[EnumMember(Value = "analyzed")]
		Analyzed,
		[EnumMember(Value = "not_analyzed")]
		NotAnalyzed,
		[EnumMember(Value = "no")]
		No
	}
}
