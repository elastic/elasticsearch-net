using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum TranslogDurability
	{
		[EnumMember(Value = "request")]
		Request,

		[EnumMember(Value = "async")]
		Async
	}
}
