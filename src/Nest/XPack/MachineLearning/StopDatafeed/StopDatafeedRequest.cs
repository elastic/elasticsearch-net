using Newtonsoft.Json;

namespace Nest
{
	public partial interface IStopDatafeedRequest
	{
		/// <summary>
		/// Controls the amount of time to wait until a datafeed stops.
		/// </summary>
		[JsonProperty("timeout")]
		Time Timeout { get; set; }

		/// <summary>
		/// If true, the datafeed is stopped forcefully.
		/// </summary>
		[JsonProperty("force")]
		bool? Force { get; set; }
	}

	/// <inheritdoc />
	public partial class StopDatafeedRequest
	{
		/// <inheritdoc />
		public Time Timeout { get; set; }

		/// <inheritdoc />
		public bool? Force { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlStopDatafeed")]
	public partial class StopDatafeedDescriptor
	{
		Time IStopDatafeedRequest.Timeout { get; set; }
		bool? IStopDatafeedRequest.Force { get; set; }

		/// <inheritdoc />
		public StopDatafeedDescriptor Timeout(Time timeout) => Assign(a => a.Timeout = timeout);

		/// <inheritdoc />
		public StopDatafeedDescriptor Force(bool? force = true) => Assign(a => a.Force = force);
	}
}
