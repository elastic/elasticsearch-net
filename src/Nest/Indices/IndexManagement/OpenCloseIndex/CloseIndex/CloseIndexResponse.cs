// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;

namespace Nest
{
	public class CloseIndexResponse : AcknowledgedResponseBase
	{
		/// <summary>
		/// Individual index responses
		/// <para />
		/// Valid only for Elasticsearch 7.3.0+
		/// </summary>
		[DataMember(Name = "indices")]
		public IReadOnlyDictionary<string, CloseIndexResult> Indices { get; internal set; } = EmptyReadOnly<string, CloseIndexResult>.Dictionary;

		/// <summary>
		/// Acknowledgement from shards
		/// <para />
		/// Valid only for Elasticsearch 7.2.0+
		/// </summary>
		[DataMember(Name = "shards_acknowledged")]
		public bool ShardsAcknowledged { get; internal set; }
	}

	[DataContract]
	public class CloseIndexResult
	{
		[DataMember(Name = "closed")]
		public bool Closed { get; internal set; }

		[DataMember(Name = "shards")]
		public IReadOnlyDictionary<string, CloseShardResult> Shards { get; internal set; } = EmptyReadOnly<string, CloseShardResult>.Dictionary;
	}

	[DataContract]
	public class CloseShardResult
	{
		[DataMember(Name = "failures")]
		public IReadOnlyCollection<ShardFailure> Failures { get; internal set; } = EmptyReadOnly<ShardFailure>.Collection;
	}
}
