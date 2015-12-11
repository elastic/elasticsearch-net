using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public abstract class GaussDecayFunction<TOrigin, TScale> : DecayFunctionBase<TOrigin, TScale>
	{
		protected override string DecayType => "gauss";
	}

	public class GaussDecayFunctionDescriptor<TOrigin, TScale, T> : DecayFunctionBaseDescriptor<GaussDecayFunctionDescriptor<TOrigin, TScale, T>, TOrigin, TScale, T>
		where T : class
	{
		protected override string DecayType => "gauss";
	}

	public class GaussDecayFunction : GaussDecayFunction<double?, double?> { }
	public class GaussDateDecayFunction : GaussDecayFunction<DateMath, TimeUnit> { }
	public class GaussGeoDecayFunction : GaussDecayFunction<GeoLocation, DistanceUnit> { }
}