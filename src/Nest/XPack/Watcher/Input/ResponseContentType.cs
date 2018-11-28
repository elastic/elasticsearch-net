using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{

	public enum ResponseContentType
	{
		[EnumMember(Value = "json")]
		Json,

		[EnumMember(Value = "yaml")]
		Yaml,

		[EnumMember(Value = "text")]
		Text
	}
}
