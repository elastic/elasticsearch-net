using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum NodeRole
	{
		[EnumMember(Value = "master")]
		Master,

		[EnumMember(Value = "data")]
		Data,

		[EnumMember(Value = "client")]
		Client,

		[EnumMember(Value = "ingest")]
		Ingest
	}
}
