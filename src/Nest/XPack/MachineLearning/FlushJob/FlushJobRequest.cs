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
	[MapsApi("ml.flush_job.json")]
	public partial interface IFlushJobRequest
	{
		/// <summary>
		/// Specifies that no data prior to this date is expected.
		/// </summary>
		[DataMember(Name ="advance_time")]
		DateTimeOffset? AdvanceTime { get; set; }

		/// <summary>
		/// Calculates the interim results for the most recent bucket or all buckets within the latency period.
		/// </summary>
		[DataMember(Name ="calc_interim")]
		bool? CalculateInterim { get; set; }

		/// <summary>
		///  When used in conjunction with <see cref="CalculateInterim" />, specifies the range of buckets on
		/// which to calculate interim results.
		/// </summary>
		[DataMember(Name ="end")]
		DateTimeOffset? End { get; set; }

		/// <summary>
		/// When used in conjunction with <see cref="CalculateInterim" />, specifies the range of buckets
		/// on which to calculate interim results.
		/// </summary>
		[DataMember(Name ="start")]
		DateTimeOffset? Start { get; set; }
	}

	public partial class FlushJobRequest
	{
		/// <inheritdoc />
		public DateTimeOffset? AdvanceTime { get; set; }

		/// <inheritdoc />
		public bool? CalculateInterim { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? End { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? Start { get; set; }
	}

	public partial class FlushJobDescriptor
	{
		DateTimeOffset? IFlushJobRequest.AdvanceTime { get; set; }
		bool? IFlushJobRequest.CalculateInterim { get; set; }
		DateTimeOffset? IFlushJobRequest.End { get; set; }
		DateTimeOffset? IFlushJobRequest.Start { get; set; }

		/// <inheritdoc />
		public FlushJobDescriptor AdvanceTime(DateTimeOffset? advanceTime) => Assign(advanceTime, (a, v) => a.AdvanceTime = v);

		/// <inheritdoc />
		public FlushJobDescriptor CalculateInterim(bool? calculateInterim = true) => Assign(calculateInterim, (a, v) => a.CalculateInterim = v);

		/// <inheritdoc />
		public FlushJobDescriptor End(DateTimeOffset? end) => Assign(end, (a, v) => a.End = v);

		/// <inheritdoc />
		public FlushJobDescriptor Start(DateTimeOffset? start) => Assign(start, (a, v) => a.Start = v);
	}
}
