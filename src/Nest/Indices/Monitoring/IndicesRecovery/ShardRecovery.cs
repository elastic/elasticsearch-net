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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	public class ShardRecovery
	{
		[DataMember(Name ="id")]
		public long Id { get; internal set; }

		[DataMember(Name ="index")]
		public RecoveryIndexStatus Index { get; internal set; }

		[DataMember(Name ="primary")]
		public bool Primary { get; internal set; }

		[DataMember(Name ="source")]
		public RecoveryOrigin Source { get; internal set; }

		[DataMember(Name ="stage")]
		public string Stage { get; internal set; }

		[Obsolete("Deprecated. Will be removed in 8.0")]
		public RecoveryStartStatus Start { get; internal set; }

		// TODO Rename property in 8.0
		[JsonFormatter(typeof(NullableDateTimeEpochMillisecondsFormatter))]
		[DataMember(Name ="start_time_in_millis")]
		public DateTime? StartTime { get; internal set; }

		// TODO Rename property in 8.0
		[JsonFormatter(typeof(NullableDateTimeEpochMillisecondsFormatter))]
		[DataMember(Name ="stop_time_in_millis")]
		public DateTime? StopTime { get; internal set; }

		[DataMember(Name ="target")]
		public RecoveryOrigin Target { get; internal set; }

		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="translog")]
		public RecoveryTranslogStatus Translog { get; internal set; }

		[DataMember(Name ="type")]
		public string Type { get; internal set; }

		[DataMember(Name ="verify_index")]
		public RecoveryVerifyIndex VerifyIndex { get; internal set; }
	}
}
