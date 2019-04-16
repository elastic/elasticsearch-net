using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
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
