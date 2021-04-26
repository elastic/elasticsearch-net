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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class IndexingStats
	{
		[DataMember(Name ="index_current")]
		public long Current { get; set; }

		[DataMember(Name ="delete_current")]
		public long DeleteCurrent { get; set; }

		[DataMember(Name ="delete_time")]
		public string DeleteTime { get; set; }

		[DataMember(Name ="delete_time_in_millis")]
		public long DeleteTimeInMilliseconds { get; set; }

		[DataMember(Name ="delete_total")]
		public long DeleteTotal { get; set; }

		[DataMember(Name ="is_throttled")]
		public bool IsThrottled { get; set; }

		[DataMember(Name ="noop_update_total")]
		public long NoopUpdateTotal { get; set; }

		[DataMember(Name ="throttle_time")]
		public string ThrottleTime { get; set; }

		[DataMember(Name ="throttle_time_in_millis")]
		public long ThrottleTimeInMilliseconds { get; set; }

		[DataMember(Name ="index_time")]
		public string Time { get; set; }

		[DataMember(Name ="index_time_in_millis")]
		public long TimeInMilliseconds { get; set; }

		[DataMember(Name ="index_total")]
		public long Total { get; set; }

		[DataMember(Name ="types")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, IndexingStats>))]
		public IReadOnlyDictionary<string, IndexingStats> Types { get; set; }
	}
}
