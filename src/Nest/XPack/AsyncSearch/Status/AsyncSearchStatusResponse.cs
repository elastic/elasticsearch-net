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

using System;
using System.Runtime.Serialization;

namespace Nest
{
	public class AsyncSearchStatusResponse : ResponseBase
	{
		[DataMember(Name = "_shards")]
		public ShardStatistics Shards { get; internal set; }

		[DataMember(Name = "completion_status")]
		public int? CompletionStatus { get; internal set; }

		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		[DataMember(Name = "is_partial")]
		public bool IsPartial { get; internal set; }

		[DataMember(Name = "start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }

		[IgnoreDataMember]
		public DateTimeOffset StartTime => DateTimeUtil.UnixEpoch.AddMilliseconds(StartTimeInMilliseconds);

		[DataMember(Name = "is_running")]
		public bool IsRunning { get; internal set; }

		[DataMember(Name = "expiration_time_in_millis")]
		public long ExpirationTimeInMilliseconds { get; internal set; }

		[IgnoreDataMember]
		public DateTimeOffset ExpirationTime => DateTimeUtil.UnixEpoch.AddMilliseconds(ExpirationTimeInMilliseconds);
	}
}
