using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ClusterAllocationExplainRequest>))]
	public partial interface IClusterAllocationExplainRequest
	{
		/// <summary>
		/// The name of the index to provide an explanation for
		/// </summary>
		[JsonProperty("index")]
		IndexName Index { get; set; }

		/// <summary>
		/// The shard id to provide an explanation for
		/// </summary>
		[JsonProperty("shard")]
		int? Shard { get; set; }

		/// <summary>
		/// Whether to explain a primary or replica shard
		/// </summary>
		[JsonProperty("primary")]
		bool? Primary { get; set; }
	}

	public partial class ClusterAllocationExplainRequest
	{
		/// <summary>
		/// The name of the index to provide an explanation for
		/// </summary>
		public IndexName Index { get; set; }

		/// <summary>
		/// The shard id to provide an explanation for
		/// </summary>
		public int? Shard { get; set; }

		/// <summary>
		/// Whether to explain a primary or replica shard
		/// </summary>
		public bool? Primary { get; set; }
	}

	public partial class ClusterAllocationExplainDescriptor
	{
		IndexName IClusterAllocationExplainRequest.Index { get; set; }
		int? IClusterAllocationExplainRequest.Shard { get; set; }
		bool? IClusterAllocationExplainRequest.Primary { get; set; }

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
