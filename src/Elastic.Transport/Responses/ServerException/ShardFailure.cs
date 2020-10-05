// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Elastic.Transport
{
	[DataContract]
	public class ShardFailure
	{
		[DataMember(Name = "index")]
		[JsonPropertyName("index")]
		public string Index { get; set; }

		[DataMember(Name = "node")]
		[JsonPropertyName("node")]
		public string Node { get; set; }

		[DataMember(Name = "reason")]
		[JsonPropertyName("reason")]
		public ErrorCause Reason { get; set; }

		[DataMember(Name = "shard")]
		[JsonPropertyName("shard")]
		public int? Shard { get; set; }

		[DataMember(Name = "status")]
		[JsonPropertyName("status")]
		public string Status { get; set; }
	}
}
