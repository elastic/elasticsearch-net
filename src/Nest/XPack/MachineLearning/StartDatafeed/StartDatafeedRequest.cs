// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[MapsApi("ml.start_datafeed.json")]
	public partial interface IStartDatafeedRequest
	{
		/// <summary>
		/// The time that the datafeed should end. This value is exclusive.
		/// </summary>
		[DataMember(Name = "end")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		DateTimeOffset? End { get; set; }

		/// <summary>
		/// The time that the datafeed should begin. This value is inclusive.
		/// </summary>
		[DataMember(Name = "start")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		DateTimeOffset? Start { get; set; }

		/// <summary>
		/// Controls the amount of time to wait until a datafeed starts.
		/// </summary>
		[DataMember(Name = "timeout")]
		Time Timeout { get; set; }
	}

	/// <inheritdoc />
	public partial class StartDatafeedRequest
	{
		/// <inheritdoc />
		public DateTimeOffset? End { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? Start { get; set; }

		/// <inheritdoc />
		public Time Timeout { get; set; }
	}

	/// <inheritdoc />
	public partial class StartDatafeedDescriptor
	{
		DateTimeOffset? IStartDatafeedRequest.End { get; set; }
		DateTimeOffset? IStartDatafeedRequest.Start { get; set; }
		Time IStartDatafeedRequest.Timeout { get; set; }

		/// <inheritdoc />
		public StartDatafeedDescriptor Timeout(Time timeout) => Assign(timeout, (a, v) => a.Timeout = v);

		/// <inheritdoc />
		public StartDatafeedDescriptor Start(DateTimeOffset? start) => Assign(start, (a, v) => a.Start = v);

		/// <inheritdoc />
		public StartDatafeedDescriptor End(DateTimeOffset? end) => Assign(end, (a, v) => a.End = v);
	}
}
