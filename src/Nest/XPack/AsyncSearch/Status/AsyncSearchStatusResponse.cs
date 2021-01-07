// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
