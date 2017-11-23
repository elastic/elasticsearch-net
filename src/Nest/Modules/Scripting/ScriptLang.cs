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

		[EnumMember(Value = "groovy")]
		Groovy,

		[EnumMember(Value = "js")]
		JS,

		[EnumMember(Value = "python")]
		Python,

		[EnumMember(Value = "expression")]
		Expression,

		[EnumMember(Value = "mustache")]
		Mustache,
	}
}
