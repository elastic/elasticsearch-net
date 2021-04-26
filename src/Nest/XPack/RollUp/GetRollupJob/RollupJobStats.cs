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
