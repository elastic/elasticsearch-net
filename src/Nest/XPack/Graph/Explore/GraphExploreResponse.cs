// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;

namespace Nest
{
	public class GraphExploreResponse : ResponseBase
	{
		[DataMember(Name ="connections")]
		public IReadOnlyCollection<GraphConnection> Connections { get; internal set; } = EmptyReadOnly<GraphConnection>.Collection;

		[DataMember(Name ="failures")]
		public IReadOnlyCollection<ShardFailure> Failures { get; internal set; } = EmptyReadOnly<ShardFailure>.Collection;

		[DataMember(Name ="timed_out")]
		public bool TimedOut { get; internal set; }

		[DataMember(Name ="took")]
		public long Took { get; internal set; }

		[DataMember(Name ="vertices")]
		public IReadOnlyCollection<GraphVertex> Vertices { get; internal set; } = EmptyReadOnly<GraphVertex>.Collection;
	}
}
