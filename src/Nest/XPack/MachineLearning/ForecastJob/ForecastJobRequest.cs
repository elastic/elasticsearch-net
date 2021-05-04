// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Uses historical behavior to predict the future behavior of a time series.
	/// </summary>
	[MapsApi("ml.forecast.json")]
	public partial interface IForecastJobRequest
	{
		/// <summary>
		/// A period of time that indicates how far into the future to forecast. Defaults to 1 day.
		/// </summary>
		[DataMember(Name ="duration")]
		Time Duration { get; set; }

		/// <summary>
		/// The period of time that forecast results are retained.
		/// After a forecast expires, the results are deleted. Defaults to 14 days.
		/// </summary>
		[DataMember(Name ="expires_in")]
		Time ExpiresIn { get; set; }
	}

	/// <inheritdoc cref="IForecastJobRequest" />
	public partial class ForecastJobRequest : IForecastJobRequest
	{
		/// <inheritdoc />
		public Time Duration { get; set; }

		/// <inheritdoc />
		public Time ExpiresIn { get; set; }
	}

	/// <inheritdoc cref="IForecastJobRequest" />
	public partial class ForecastJobDescriptor
	{
		Time IForecastJobRequest.Duration { get; set; }
		Time IForecastJobRequest.ExpiresIn { get; set; }

		/// <inheritdoc cref="IForecastJobRequest.Duration" />
		public ForecastJobDescriptor Duration(Time duration) => Assign(duration, (a, v) => a.Duration = v);

		/// <inheritdoc cref="IForecastJobRequest.ExpiresIn" />
		public ForecastJobDescriptor ExpiresIn(Time expiresIn) => Assign(expiresIn, (a, v) => a.ExpiresIn = v);
	}
}
