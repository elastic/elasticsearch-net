using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum LifecycleOperationMode
	{
		[EnumMember(Value = "RUNNING")] Running,
		[EnumMember(Value = "STOPPING")] Stopping,
		[EnumMember(Value = "STOPPED")] Stopped
	}
}
