using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Linq;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ICancelTasksResponse: IResponse
	{
		[JsonProperty("nodes")]
		IReadOnlyDictionary<string, TaskExecutingNode> Nodes { get; }

		[JsonProperty("node_failures")]
		IReadOnlyCollection<Throwable> NodeFailures { get; }
	}

	public class CancelTasksResponse : ResponseBase, ICancelTasksResponse
	{
		public override bool IsValid => base.IsValid && !this.NodeFailures.HasAny();

		public IReadOnlyDictionary<string, TaskExecutingNode> Nodes { get; internal set; } = EmptyReadOnly<string, TaskExecutingNode>.Dictionary;
		public IReadOnlyCollection<Throwable> NodeFailures { get; internal set; } = EmptyReadOnly<Throwable>.Collection;
	}
}
