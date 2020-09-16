// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Elasticsearch.Net
{
	[DataContract]
	public class ShardFailure
	{
		[DataMember(Name = "index")]
		public string Index { get; set; }

		[DataMember(Name = "node")]
		public string Node { get; set; }

		[DataMember(Name = "reason")]
		public ErrorCause Reason { get; set; }

		[DataMember(Name = "shard")]
		public int? Shard { get; set; }

		[DataMember(Name = "status")]
		public string Status { get; set; }
	}
}
