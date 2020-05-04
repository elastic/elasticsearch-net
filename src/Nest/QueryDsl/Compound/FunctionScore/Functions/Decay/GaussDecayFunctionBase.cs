// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public abstract class GaussDecayFunctionBase<TOrigin, TScale> : DecayFunctionBase<TOrigin, TScale>
	{
		protected override string DecayType => "gauss";
	}

	public class GaussDecayFunctionDescriptor<TOrigin, TScale, T>
		: DecayFunctionDescriptorBase<GaussDecayFunctionDescriptor<TOrigin, TScale, T>, TOrigin, TScale, T>
		where T : class
	{
		protected override string DecayType => "gauss";
	}

	public class GaussDecayFunction : GaussDecayFunctionBase<double?, double?> { }

	public class GaussDateDecayFunction : GaussDecayFunctionBase<DateMath, Time> { }

	public class GaussGeoDecayFunction : GaussDecayFunctionBase<GeoLocation, Distance> { }
}
