using Newtonsoft.Json;
// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Nest
{
	[JsonObject]
	public class CatAllocationRecord : ICatRecord
	{
		/// <summary>
		/// Amount of disk available
		/// </summary>
		[JsonProperty("disk.avail")]
		public string DiskAvailable { get; set; }

		/// <summary>
		/// Amount of disk used by Elasticsearch indices
		/// </summary>
		[JsonProperty("disk.indices")]
		public string DiskIndices { get; set; }

		/// <summary>
		/// The percentage of disk used
		/// </summary>
		[JsonProperty("disk.percent")]
		public string DiskPercent { get; set; }

		[Obsolete("Use DiskPercent, DiskTotal, DiskAvailable, DiskTotal and DiskIndices")]
		[JsonIgnore]
		public string DiskRatio { get; set; }

		/// <summary>
		/// Total capacity of all volumes
		/// </summary>
		[JsonProperty("disk.total")]
		public string DiskTotal { get; set; }

		/// <summary>
		/// Amount of disk used (total, not just Elasticsearch)
		/// </summary>
		[JsonProperty("disk.used")]
		public string DiskUsed { get; set; }

		/// <summary>
		/// The host of the node
		/// </summary>
		[JsonProperty("host")]
		public string Host { get; set; }

		/// <summary>
		/// The IP address of the node
		/// </summary>
		[JsonProperty("ip")]
		public string Ip { get; set; }

		/// <summary>
		/// The name of the node
		/// </summary>
		[JsonProperty("node")]
		public string Node { get; set; }

		/// <summary>
		/// Number of shards on the node
		/// </summary>
		[JsonProperty("shards")]
		public string Shards { get; set; }
	}
}
