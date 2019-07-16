using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum FollowerIndexStatus
	{
		[EnumMember(Value = "active")]
		Active,

		[EnumMember(Value = "paused")]
		Paused
	}
}
