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
		IDictionary<string, TaskExecutingNode> Nodes { get; }

		[JsonProperty("node_failures")]
		IEnumerable<Throwable> NodeFailures { get; }
	}

	public class CancelTasksResponse : ResponseBase, ICancelTasksResponse
	{
		public override bool IsValid => base.IsValid && !this.NodeFailures.HasAny();

		public IDictionary<string, TaskExecutingNode> Nodes { get; internal set; }
		public IEnumerable<Throwable> NodeFailures { get; internal set; }
	}
}
