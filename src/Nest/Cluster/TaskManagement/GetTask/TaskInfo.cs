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
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class TaskInfo
	{
		[DataMember(Name ="action")]
		public string Action { get; internal set; }

		[DataMember(Name ="cancellable")]
		public bool Cancellable { get; internal set; }

		[DataMember(Name ="children")]
		public IReadOnlyCollection<TaskInfo> Children { get; internal set; } = EmptyReadOnly<TaskInfo>.Collection;

		[DataMember(Name ="description")]
		public string Description { get; internal set; }

		[DataMember(Name ="headers")]
		public IReadOnlyDictionary<string, string> Headers { get; internal set; } = EmptyReadOnly<string, string>.Dictionary;

		[DataMember(Name ="id")]
		public long Id { get; internal set; }

		[DataMember(Name ="node")]
		public string Node { get; internal set; }

		[DataMember(Name ="running_time_in_nanos")]
		public long RunningTimeInNanoseconds { get; internal set; }

		[DataMember(Name ="start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="status")]
		public TaskStatus Status { get; internal set; }

		[DataMember(Name ="type")]
		public string Type { get; internal set; }
	}
}
