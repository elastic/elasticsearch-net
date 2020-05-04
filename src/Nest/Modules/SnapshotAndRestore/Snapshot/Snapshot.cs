// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class Snapshot
	{
		[DataMember(Name ="duration_in_millis")]
		public long DurationInMilliseconds { get; internal set; }

		[DataMember(Name ="end_time")]
		public DateTime EndTime { get; internal set; }

		[DataMember(Name ="end_time_in_millis")]
		public long EndTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="failures")]
		public IReadOnlyCollection<SnapshotShardFailure> Failures { get; internal set; }

		[DataMember(Name ="indices")]
		public IReadOnlyCollection<IndexName> Indices { get; internal set; }

		[DataMember(Name ="snapshot")]
		public string Name { get; internal set; }

		[DataMember(Name ="shards")]
		public ShardStatistics Shards { get; internal set; }

		[DataMember(Name ="start_time")]
		public DateTime StartTime { get; internal set; }

		[DataMember(Name ="start_time_in_millis")]
		public long StartTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="state")]
		public string State { get; internal set; }
		
		[DataMember(Name ="metadata")]
		public IReadOnlyDictionary<string, object> Metadata { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}
