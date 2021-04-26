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
using Nest.Utf8Json;

namespace Nest
{
	public class ReindexNode
	{
		[DataMember(Name ="attributes")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, string>))]
		public IReadOnlyDictionary<string, string> Attributes { get; internal set; } =
			EmptyReadOnly<string, string>.Dictionary;

		[DataMember(Name ="host")]
		public string Host { get; internal set; }

		[DataMember(Name ="ip")]
		public string Ip { get; internal set; }

		[DataMember(Name ="name")]
		public string Name { get; internal set; }

		[DataMember(Name ="roles")]
		public IEnumerable<string> Roles { get; internal set; }

		[DataMember(Name ="tasks")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<TaskId, ReindexTask>))]
		public IReadOnlyDictionary<TaskId, ReindexTask> Tasks { get; internal set; } =
			EmptyReadOnly<TaskId, ReindexTask>.Dictionary;

		[DataMember(Name ="transport_address")]
		public string TransportAddress { get; internal set; }
	}


	public class ReindexTask
	{
		[DataMember(Name ="action")]
		public string Action { get; internal set; }

		[DataMember(Name ="cancellable")]
		public bool Cancellable { get; internal set; }

		[DataMember(Name ="description")]
		public string Description { get; internal set; }

		[DataMember(Name ="id")]
		public long Id { get; internal set; }

		[DataMember(Name ="node")]
		public string Node { get; internal set; }

		[DataMember(Name ="running_time_in_nanos")]
		public long RunningTimeInNanoseconds { get; internal set; }

		[DataMember(Name ="start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="status")]
		public ReindexStatus Status { get; internal set; }

		[DataMember(Name ="type")]
		public string Type { get; internal set; }
	}

	public class ReindexStatus
	{
		[DataMember(Name ="batches")]
		public long Batches { get; internal set; }

		[DataMember(Name ="created")]
		public long Created { get; internal set; }

		[DataMember(Name ="deleted")]
		public long Deleted { get; internal set; }

		[DataMember(Name ="noops")]
		public long Noops { get; internal set; }

		[DataMember(Name ="requests_per_second")]
		public float RequestsPerSecond { get; internal set; }

		[DataMember(Name ="retries")]
		public Retries Retries { get; internal set; }

		[DataMember(Name ="throttled_millis")]
		public long ThrottledInMilliseconds { get; internal set; }

		[DataMember(Name ="throttled_until_millis")]
		public long ThrottledUntilInMilliseconds { get; internal set; }

		[DataMember(Name ="total")]
		public long Total { get; internal set; }

		[DataMember(Name ="updated")]
		public long Updated { get; internal set; }

		[DataMember(Name ="version_conflicts")]
		public long VersionConflicts { get; internal set; }
	}
}
