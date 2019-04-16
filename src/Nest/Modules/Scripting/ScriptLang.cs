using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
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
