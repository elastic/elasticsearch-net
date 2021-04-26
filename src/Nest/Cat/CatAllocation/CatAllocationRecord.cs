/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
