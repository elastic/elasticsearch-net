// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class RollupJobStats
	{
		[DataMember(Name ="documents_processed")]
		public long DocumentsProcessed { get; internal set; }

		[DataMember(Name ="pages_processed")]
		public long PagesProcessed { get; internal set; }

		[DataMember(Name ="rollups_indexed")]
		public long RollupsIndexed { get; internal set; }

		[DataMember(Name ="trigger_count")]
		public long TriggerCount { get; internal set; }

		[DataMember(Name = "search_failures")]
		public long? SearchFailures { get; internal set; }

		[DataMember(Name = "index_failures")]
		public long? IndexFailures { get; internal set; }

		[DataMember(Name = "index_time_in_ms")]
		public long? IndexTimeInMilliseconds { get; internal set; }

		[DataMember(Name = "index_total")]
		public long? IndexTotal { get; internal set; }

		[DataMember(Name = "search_time_in_ms")]
		public long? SearchTimeInMilliseconds { get; internal set; }

		[DataMember(Name = "search_total")]
		public long? SearchTotal { get; internal set; }
	}
}
