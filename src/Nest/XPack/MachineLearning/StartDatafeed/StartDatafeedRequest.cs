using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IStartDatafeedRequest
	{
		/// <summary>
		/// Controls the amount of time to wait until a datafeed starts.
		/// </summary>
		[JsonProperty("timeout")]
		Time Timeout { get; set; }

		/// <summary>
		/// The time that the datafeed should begin. This value is inclusive.
		/// </summary>
		[JsonProperty("start")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		DateTimeOffset? Start { get; set; }

		/// <summary>
		/// The time that the datafeed should end. This value is exclusive.
		/// </summary>
		[JsonProperty("end")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		DateTimeOffset? End { get; set; }
	}

	/// <inheritdoc />
	public partial class StartDatafeedRequest
	{
		/// <inheritdoc />
		public Time Timeout { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? Start { get; set; }

		/// <inheritdoc />
		public DateTimeOffset? End { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlStartDatafeed")]
	public partial class StartDatafeedDescriptor
	{
		Time IStartDatafeedRequest.Timeout { get; set; }
		DateTimeOffset? IStartDatafeedRequest.Start { get; set; }
		DateTimeOffset? IStartDatafeedRequest.End { get; set; }

		/// <inheritdoc />
		public StartDatafeedDescriptor Timeout(Time timeout) => Assign(a => a.Timeout = timeout);

		/// <inheritdoc />
		public StartDatafeedDescriptor Start(DateTimeOffset? start) => Assign(a => a.Start = start);

		/// <inheritdoc />
		public StartDatafeedDescriptor End(DateTimeOffset? end) => Assign(a => a.End = end);
	}
}
