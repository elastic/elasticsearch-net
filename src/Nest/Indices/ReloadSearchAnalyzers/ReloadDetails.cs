// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

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
