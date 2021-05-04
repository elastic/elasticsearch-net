// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public abstract class LinearDecayFunctionBase<TOrigin, TScale> : DecayFunctionBase<TOrigin, TScale>
	{
		protected override string DecayType => "linear";
	}

	public class LinearDecayFunctionDescriptor<TOrigin, TScale, T>
		: DecayFunctionDescriptorBase<LinearDecayFunctionDescriptor<TOrigin, TScale, T>, TOrigin, TScale, T>
		where T : class
	{
		protected override string DecayType => "linear";
	}

	public class LinearDecayFunction : LinearDecayFunctionBase<double?, double?> { }

	public class LinearDateDecayFunction : LinearDecayFunctionBase<DateMath, Time> { }

	public class LinearGeoDecayFunction : LinearDecayFunctionBase<GeoLocation, Distance> { }
}
