// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("cluster.allocation_explain.json")]
	[ReadAs(typeof(ClusterAllocationExplainRequest))]
	public partial interface IClusterAllocationExplainRequest
	{
		/// <summary>
		/// The name of the index to provide an explanation for
		/// </summary>
		[DataMember(Name ="index")]
		IndexName Index { get; set; }

		/// <summary>
		/// Whether to explain a primary or replica shard
		/// </summary>
		[DataMember(Name ="primary")]
		bool? Primary { get; set; }

		/// <summary>
		/// The shard id to provide an explanation for
		/// </summary>
		[DataMember(Name ="shard")]
		int? Shard { get; set; }
	}

	public partial class ClusterAllocationExplainRequest
	{
		/// <summary>
		/// The name of the index to provide an explanation for
		/// </summary>
		public IndexName Index { get; set; }

		/// <summary>
		/// Whether to explain a primary or replica shard
		/// </summary>
		public bool? Primary { get; set; }

		/// <summary>
		/// The shard id to provide an explanation for
		/// </summary>
		public int? Shard { get; set; }
	}

	public partial class ClusterAllocationExplainDescriptor
	{
		IndexName IClusterAllocationExplainRequest.Index { get; set; }
		bool? IClusterAllocationExplainRequest.Primary { get; set; }
		int? IClusterAllocationExplainRequest.Shard { get; set; }

		/// <summary>
		/// The name of the index to provide an explanation for
		/// </summary>
		public ClusterAllocationExplainDescriptor Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		/// <summary>
		/// The name of the index to provide an explanation for
		/// </summary>
		public ClusterAllocationExplainDescriptor Index<TDocument>() => Assign(typeof(TDocument), (a, v) => a.Index = v);

		/// <summary>
		/// Whether to explain a primary or replica shard
		/// </summary>
		public ClusterAllocationExplainDescriptor Primary(bool? primary = true) => Assign(primary, (a, v) => a.Primary = v);

		/// <summary>
		/// The shard id to provide an explanation for
		/// </summary>
		public ClusterAllocationExplainDescriptor Shard(int? shard) => Assign(shard, (a, v) => a.Shard = v);
	}
}
