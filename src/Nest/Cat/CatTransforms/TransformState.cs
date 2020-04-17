using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum TransformState
	{
		[EnumMember(Value = "STARTED")] Started,
		[EnumMember(Value = "INDEXING")] Indexing,
		[EnumMember(Value = "ABORTING")] Aborting,
		[EnumMember(Value = "STOPPING")] Stopping,
		[EnumMember(Value = "STOPPED")] Stopped,
		[EnumMember(Value = "FAILED")] Failed
	}
}
