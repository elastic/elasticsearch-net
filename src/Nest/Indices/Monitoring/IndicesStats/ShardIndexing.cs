// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardIndexing
	{
		/// <summary>
		/// Returns the currently in-flight delete operations
		/// </summary>
		[DataMember(Name ="delete_current")]
		public long DeleteCurrent { get; internal set; }

		/// <summary>
		/// The total amount of time spend on executing delete operations.
		/// </summary>
		[DataMember(Name ="delete_time_in_millis")]
		public long DeleteTimeInMilliseconds { get; internal set; }

		/// <summary>
		/// Returns the number of delete operation executed
		/// </summary>
		[DataMember(Name ="delete_total")]
		public long DeleteTotal { get; internal set; }

		/// <summary>
		/// Returns the currently in-flight indexing operations.
		/// </summary>
		[DataMember(Name ="index_current")]
		public long IndexCurrent { get; internal set; }

		/// <summary>
		/// The number of failed indexing operations
		/// </summary>
		[DataMember(Name ="index_failed")]
		public long IndexFailed { get; internal set; }

		/// <summary>
		/// The total amount of time spend on executing index operations.
		/// </summary>
		[DataMember(Name ="index_time_in_millis")]
		public long IndexTimeInMilliseconds { get; internal set; }

		/// <summary>
		/// The total number of indexing operations
		/// </summary>
		[DataMember(Name ="index_total")]
		public long IndexTotal { get; internal set; }

		/// <summary>
		/// Returns if the index is under merge throttling control
		/// </summary>
		[DataMember(Name ="is_throttled")]
		public bool IsThrottled { get; internal set; }

		/// <summary>
		/// Returns the number of noop update operations
		/// </summary>
		[DataMember(Name ="noop_update_total")]
		public long NoopUpdateTotal { get; internal set; }

		/// <summary>
		/// Gets the amount of time that the index has been under merge throttling control
		/// </summary>
		[DataMember(Name ="throttle_time_in_millis")]
		public long ThrottleTimeInMilliseconds { get; internal set; }
	}
}
