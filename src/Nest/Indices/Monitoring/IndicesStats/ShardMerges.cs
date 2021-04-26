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
	public class ShardMerges
	{
		[DataMember(Name ="current")]
		public long Current { get; internal set; }

		[DataMember(Name ="current_docs")]
		public long CurrentDocuments { get; internal set; }

		[DataMember(Name ="current_size_in_bytes")]
		public long CurrentSizeInBytes { get; internal set; }

		[DataMember(Name ="total")]
		public long Total { get; internal set; }

		[DataMember(Name ="total_auto_throttle_in_bytes")]
		public long TotalAutoThrottleInBytes { get; internal set; }

		[DataMember(Name ="total_docs")]
		public long TotalDocuments { get; internal set; }

		[DataMember(Name ="total_size_in_bytes")]
		public long TotalSizeInBytes { get; internal set; }

		[DataMember(Name ="total_stopped_time_in_millis")]
		public long TotalStoppedTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="total_throttled_time_in_millis")]
		public long TotalThrottledTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }
	}
}
