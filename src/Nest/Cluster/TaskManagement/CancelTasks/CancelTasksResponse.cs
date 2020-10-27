// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;

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
