// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("ml.stop_datafeed.json")]
	public partial interface IStopDatafeedRequest
	{
		/// <summary>
		/// If true, the datafeed is stopped forcefully.
		/// </summary>
		[DataMember(Name ="force")]
		bool? Force { get; set; }

		/// <summary>
		/// Controls the amount of time to wait until a datafeed stops.
		/// </summary>
		[DataMember(Name ="timeout")]
		Time Timeout { get; set; }
	}

	/// <inheritdoc />
	public partial class StopDatafeedRequest
	{
		/// <inheritdoc />
		public bool? Force { get; set; }

		/// <inheritdoc />
		public Time Timeout { get; set; }
	}

	/// <inheritdoc />
	public partial class StopDatafeedDescriptor
	{
		bool? IStopDatafeedRequest.Force { get; set; }
		Time IStopDatafeedRequest.Timeout { get; set; }

		/// <inheritdoc />
		public StopDatafeedDescriptor Timeout(Time timeout) => Assign(timeout, (a, v) => a.Timeout = v);

		/// <inheritdoc />
		public StopDatafeedDescriptor Force(bool? force = true) => Assign(force, (a, v) => a.Force = v);
	}
}
