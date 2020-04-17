using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class ReloadDetails
	{
		[DataMember(Name ="index")]
		public string Index { get; internal set; }

		[DataMember(Name ="reloaded_analyzers")]
		public IReadOnlyCollection<string> ReloadedAnalyzers { get; internal set; }  = EmptyReadOnly<string>.Collection;

		[DataMember(Name ="reloaded_node_ids")]
		public IReadOnlyCollection<string> ReloadedNodeIds { get; internal set; }  = EmptyReadOnly<string>.Collection;
	}
}
