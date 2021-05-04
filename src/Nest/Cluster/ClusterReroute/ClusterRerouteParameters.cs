// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ClusterRerouteParameters
	{
		[DataMember(Name ="allow_primary")]
		public bool? AllowPrimary { get; set; }

		[DataMember(Name ="from_node")]
		public string FromNode { get; set; }

		[DataMember(Name ="index")]
		public string Index { get; set; }

		[DataMember(Name ="node")]
		public string Node { get; set; }

		[DataMember(Name ="shard")]
		public int Shard { get; set; }

		[DataMember(Name ="to_node")]
		public string ToNode { get; set; }
	}
}
