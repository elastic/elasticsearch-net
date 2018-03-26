namespace Nest
{
	/// <summary>
	/// const string collection of known Elasticsearch index settings that can only be provided at
	/// index creation time
	/// </summary>
	public static class FixedIndexSettings
	{
		public const string NumberOfShards = "index.number_of_shards";
		public const string NumberOfRoutingShards = "index.number_of_routing_shards";
		public const string RoutingPartitionSize = "index.routing_partition_size";
	}
}
