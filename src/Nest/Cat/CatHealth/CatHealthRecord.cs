// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatHealthRecord : ICatRecord
	{
		[DataMember(Name ="cluster")]
		public string Cluster { get; set; }

		[DataMember(Name ="epoch")]
		public string Epoch { get; set; }

		[DataMember(Name ="init")]
		public string Initializing { get; set; }

		[DataMember(Name ="node.data")]
		public string NodeData { get; set; }

		[DataMember(Name ="node.total")]
		public string NodeTotal { get; set; }

		[DataMember(Name ="pending_tasks")]
		public string PendingTasks { get; set; }

		[DataMember(Name ="pri")]
		public string Primary { get; set; }

		[DataMember(Name ="relo")]
		public string Relocating { get; set; }

		[DataMember(Name ="shards")]
		public string Shards { get; set; }

		[DataMember(Name ="status")]
		public string Status { get; set; }

		[DataMember(Name ="timestamp")]
		public string Timestamp { get; set; }

		[DataMember(Name ="unassign")]
		public string Unassigned { get; set; }
	}
}
