using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ScriptLang
	{
		[EnumMember(Value = "painless")]
		Painless,

		[EnumMember(Value = "expression")]
		Expression,

		[EnumMember(Value = "mustache")]
		Mustache,
	}
}
