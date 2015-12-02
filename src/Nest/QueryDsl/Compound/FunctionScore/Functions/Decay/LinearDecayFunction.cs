using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public abstract class LinearDecayFunction<TOrigin, TScale> : DecayFunctionBase<TOrigin, TScale>
	{
		protected override string DecayType => "linear";
	}
	public class LinearDecayFunctionDescriptor<TOrigin, TScale, T> : DecayFunctionBaseDescriptor<LinearDecayFunctionDescriptor<TOrigin, TScale, T>, TOrigin, TScale, T>
		where T : class
	{
		protected override string DecayType => "linear";
	}

	public class LinearDecayFunction : LinearDecayFunction<double?, double?> { }
	public class LinearDateDecayFunction : LinearDecayFunction<DateMath, TimeUnitExpression> { }
	public class LinearGeoDecayFunction : LinearDecayFunction<GeoLocation, GeoDistance> { }
}