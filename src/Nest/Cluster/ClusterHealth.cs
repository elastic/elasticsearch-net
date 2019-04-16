using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum ClusterStatus
	{
		[EnumMember(Value = "green")]
		Green,

		[EnumMember(Value = "yellow")]
		Yellow,

		[EnumMember(Value = "red")]
		Red
	}
}
