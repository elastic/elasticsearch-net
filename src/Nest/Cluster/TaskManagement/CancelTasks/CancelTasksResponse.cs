using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class CancelTasksResponse : ResponseBase
	{
		public override bool IsValid => base.IsValid && !NodeFailures.HasAny();

		[DataMember(Name = "node_failures")]
		public IReadOnlyCollection<ErrorCause> NodeFailures { get; internal set; } = EmptyReadOnly<ErrorCause>.Collection;

		[DataMember(Name = "nodes")]
		public IReadOnlyDictionary<string, TaskExecutingNode> Nodes { get; internal set; } = EmptyReadOnly<string, TaskExecutingNode>.Dictionary;
	}

}
