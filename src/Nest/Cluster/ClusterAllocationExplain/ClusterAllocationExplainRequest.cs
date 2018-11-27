using System.Runtime.Serialization;

namespace Nest
{
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
		public ClusterAllocationExplainDescriptor Index(IndexName index) => Assign(a => a.Index = index);

		/// <summary>
		/// The name of the index to provide an explanation for
		/// </summary>
		public ClusterAllocationExplainDescriptor Index<TDocument>() => Assign(a => a.Index = typeof(TDocument));

		/// <summary>
		/// Whether to explain a primary or replica shard
		/// </summary>
		public ClusterAllocationExplainDescriptor Primary(bool? primary = true) => Assign(a => a.Primary = primary);

		/// <summary>
		/// The shard id to provide an explanation for
		/// </summary>
		public ClusterAllocationExplainDescriptor Shard(int? shard) => Assign(a => a.Shard = shard);
	}
}
