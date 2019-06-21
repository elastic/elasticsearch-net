namespace Nest
{
	/// <summary>
	/// const string collection of known Elasticsearch index settings that can only be provided at
	/// index creation time
	/// </summary>
	public static class FixedIndexSettings
	{
		public const string NumberOfRoutingShards = "index.number_of_routing_shards";
		public const string NumberOfShards = "index.number_of_shards";
		public const string RoutingPartitionSize = "index.routing_partition_size";

		/// <summary>
		/// If a field referred to in a percolator query does not exist,
		/// it will be handled as a default text field so that adding the percolator query doesn't fail.
		/// Defaults to <c>false</c>
		/// </summary>
		public const string PercolatorMapUnmappedFieldsAsText = "index.percolator.map_unmapped_fields_as_text";
	}
}
