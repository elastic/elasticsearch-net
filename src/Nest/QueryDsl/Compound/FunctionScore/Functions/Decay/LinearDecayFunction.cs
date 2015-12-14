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
	public class LinearDateDecayFunction : LinearDecayFunction<DateMath, Time> { }
	public class LinearGeoDecayFunction : LinearDecayFunction<GeoLocation, Distance> { }
}