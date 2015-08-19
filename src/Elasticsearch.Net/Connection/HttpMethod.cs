using System.Runtime.Serialization;

namespace Elasticsearch.Net
{
	public enum HttpMethod
	{
		[EnumMember(Value = "GET")]
		GET,
		[EnumMember(Value = "POST")]
		POST,
		[EnumMember(Value = "PUT")]
		PUT,
		[EnumMember(Value = "DELETE")]
		DELETE,
		[EnumMember(Value = "HEAD")]
		HEAD
	}
}