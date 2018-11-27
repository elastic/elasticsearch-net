using System.Collections.Generic;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface ICancelTasksResponse : IResponse
	{
		[DataMember(Name ="node_failures")]
		IReadOnlyCollection<ErrorCause> NodeFailures { get; }

		[DataMember(Name ="nodes")]
		IReadOnlyDictionary<string, TaskExecutingNode> Nodes { get; }
	}

	public class CancelTasksResponse : ResponseBase, ICancelTasksResponse
	{
		public override bool IsValid => base.IsValid && !NodeFailures.HasAny();
		public IReadOnlyCollection<ErrorCause> NodeFailures { get; internal set; } = EmptyReadOnly<ErrorCause>.Collection;

		public IReadOnlyDictionary<string, TaskExecutingNode> Nodes { get; internal set; } = EmptyReadOnly<string, TaskExecutingNode>.Dictionary;
	}
}
