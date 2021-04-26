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
	public class RecoveryIndexStatus
	{
		[Obsolete("Deprecated. Use Size instead. Will be removed in 8.0")]
		public RecoveryBytes Bytes => Size;

		[DataMember(Name = "files")]
		public RecoveryFiles Files { get; internal set; }

		[DataMember(Name = "size")]
		public RecoveryBytes Size { get; internal set; }

		[DataMember(Name = "source_throttle_time_in_millis")]
		public long SourceThrottleTimeInMilliseconds { get; internal set; }

		[DataMember(Name = "target_throttle_time_in_millis")]
		public long TargetThrottleTimeInMilliseconds { get; internal set; }

		[DataMember(Name = "total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }
	}
}
