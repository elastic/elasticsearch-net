using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Uses historical behavior to predict the future behavior of a time series.
	/// </summary>
	public partial interface IForecastJobRequest
	{
		/// <summary>
		/// A period of time that indicates how far into the future to forecast. Defaults to 1 day.
		/// </summary>
		[JsonProperty("duration")]
		Time Duration { get; set; }

		/// <summary>
		/// The period of time that forecast results are retained.
		/// After a forecast expires, the results are deleted. Defaults to 14 days.
		/// </summary>
		[JsonProperty("expires_in")]
		Time ExpiresIn { get; set; }
	}

	/// <inheritdoc cref="IForecastJobRequest"/>
	public partial class ForecastJobRequest : IForecastJobRequest
	{
		/// <inheritdoc />
		public Time Duration { get; set; }

		/// <inheritdoc />
		public Time ExpiresIn { get; set; }
	}

	/// <inheritdoc cref="IForecastJobRequest" />
	[DescriptorFor("XpackMlForecast")]
	public partial class ForecastJobDescriptor
	{
		Time IForecastJobRequest.Duration { get; set; }
		Time IForecastJobRequest.ExpiresIn { get; set; }

		/// <inheritdoc cref="IForecastJobRequest.Duration" />
		public ForecastJobDescriptor Duration(Time duration) => Assign(a => a.Duration = duration);

		/// <inheritdoc cref="IForecastJobRequest.ExpiresIn" />
		public ForecastJobDescriptor ExpiresIn(Time expiresIn) => Assign(a => a.ExpiresIn = expiresIn);
	}
}
