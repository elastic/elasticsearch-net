using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{

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
