using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IClusterPendingTasksResponse : IResponse
	{
		IReadOnlyCollection<PendingTask> Tasks { get; }
	}

	[DataContract]
	public class ClusterPendingTasksResponse : ResponseBase, IClusterPendingTasksResponse
	{
		[DataMember(Name ="tasks")]
		public IReadOnlyCollection<PendingTask> Tasks { get; internal set; } = EmptyReadOnly<PendingTask>.Collection;
	}

	[DataContract]
	public class PendingTask
	{
		[DataMember(Name ="insert_order")]
		public int InsertOrder { get; internal set; }

		[DataMember(Name ="priority")]
		public string Priority { get; internal set; }

		[DataMember(Name ="source")]
		public string Source { get; internal set; }

		[DataMember(Name ="time_in_queue")]
		public string TimeInQueue { get; internal set; }

		[DataMember(Name ="time_in_queue_millis")]
		public int TimeInQueueMilliseconds { get; internal set; }
	}
}
