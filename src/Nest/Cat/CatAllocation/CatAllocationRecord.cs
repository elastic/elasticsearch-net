// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatAllocationRecord : ICatRecord
	{
		/// <summary>
		/// Amount of disk available
		/// </summary>
		[DataMember(Name ="disk.avail")]
		public string DiskAvailable { get; set; }

		/// <summary>
		/// Amount of disk used by Elasticsearch indices
		/// </summary>
		[DataMember(Name ="disk.indices")]
		public string DiskIndices { get; set; }

		/// <summary>
		/// The percentage of disk used
		/// </summary>
		[DataMember(Name ="disk.percent")]
		public string DiskPercent { get; set; }

		/// <summary>
		/// Total capacity of all volumes
		/// </summary>
		[DataMember(Name ="disk.total")]
		public string DiskTotal { get; set; }

		/// <summary>
		/// Amount of disk used (total, not just Elasticsearch)
		/// </summary>
		[DataMember(Name ="disk.used")]
		public string DiskUsed { get; set; }

		/// <summary>
		/// The host of the node
		/// </summary>
		[DataMember(Name ="host")]
		public string Host { get; set; }

		/// <summary>
		/// The IP address of the node
		/// </summary>
		[DataMember(Name ="ip")]
		public string Ip { get; set; }

		/// <summary>
		/// The name of the node
		/// </summary>
		[DataMember(Name ="node")]
		public string Node { get; set; }

		/// <summary>
		/// Number of shards on the node
		/// </summary>
		[DataMember(Name ="shards")]
		public string Shards { get; set; }
	}
}
