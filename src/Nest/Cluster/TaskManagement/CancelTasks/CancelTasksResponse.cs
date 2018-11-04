using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ICancelTasksResponse : IResponse
	{
		[JsonProperty("node_failures")]
		IReadOnlyCollection<Throwable> NodeFailures { get; }

		[JsonProperty("nodes")]
		IReadOnlyDictionary<string, TaskExecutingNode> Nodes { get; }
	}

	public class CancelTasksResponse : ResponseBase, ICancelTasksResponse
	{
		public override bool IsValid => base.IsValid && !NodeFailures.HasAny();
		public IReadOnlyCollection<Throwable> NodeFailures { get; internal set; } = EmptyReadOnly<Throwable>.Collection;

		public IReadOnlyDictionary<string, TaskExecutingNode> Nodes { get; internal set; } = EmptyReadOnly<string, TaskExecutingNode>.Dictionary;
	}
}
