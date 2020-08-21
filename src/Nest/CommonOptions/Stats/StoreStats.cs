// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class StoreStats
	{
		/// <summary>
		/// Total size of all shards assigned to the node.
		/// </summary>
		[DataMember(Name = "size")]
		public string Size { get; set; }

		/// <summary>
		/// Total size, in bytes, of all shards assigned to the node.
		/// </summary>
		// TODO: should be long
		[DataMember(Name = "size_in_bytes")]
		public double SizeInBytes { get; set; }

		/// <summary>
		/// A prediction of how much larger the shard stores on this node will eventually grow due to ongoing peer recoveries, restoring snapshots,
		/// and similar activities. A value of -1b indicates that this is not available.
		/// <para />
		/// Valid in Elasticsearch 7.9.0+
		/// </summary>
		[DataMember(Name = "reserved")]
		public string Reserved { get; set; }

		/// <summary>
		/// A prediction, in bytes, of how much larger the shard stores on this node will eventually grow due to ongoing peer recoveries,
		/// restoring snapshots, and similar activities. A value of -1 indicates that this is not available.
		/// <para />
		/// Valid in Elasticsearch 7.9.0+
		/// </summary>
		[DataMember(Name = "reserved_in_bytes")]
		public long ReservedInBytes { get; set; }
	}
}
