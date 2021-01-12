// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;

namespace Nest
{
	public abstract class NodesResponseBase : ResponseBase
	{
		[DataMember(Name = "_nodes")]
		public NodeStatistics NodeStatistics { get; internal set; }
	}

	[DataContract]
	public class NodeStatistics
	{
		[DataMember(Name = "failed")]
		public int Failed { get; internal set; }

		[DataMember(Name = "successful")]
		public int Successful { get; internal set; }

		[DataMember(Name = "total")]
		public int Total { get; internal set; }

		[DataMember(Name = "failures")]
		public IReadOnlyCollection<ErrorCause> Failures { get; internal set; } = EmptyReadOnly<ErrorCause>.Collection;
	}
}
