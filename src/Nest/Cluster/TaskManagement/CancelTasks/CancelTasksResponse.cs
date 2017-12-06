using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ICancelTasksResponse: IResponse
	{
		[JsonProperty("nodes")]
		IReadOnlyDictionary<string, TaskExecutingNode> Nodes { get; }

		[JsonProperty("node_failures")]
		IReadOnlyCollection<ErrorCause> NodeFailures { get; }
	}

	public class CancelTasksResponse : ResponseBase, ICancelTasksResponse
	{
		public override bool IsValid => base.IsValid && !this.NodeFailures.HasAny();

		public IReadOnlyDictionary<string, TaskExecutingNode> Nodes { get; internal set; } = EmptyReadOnly<string, TaskExecutingNode>.Dictionary;
		public IReadOnlyCollection<ErrorCause> NodeFailures { get; internal set; } = EmptyReadOnly<ErrorCause>.Collection;
	}
}
