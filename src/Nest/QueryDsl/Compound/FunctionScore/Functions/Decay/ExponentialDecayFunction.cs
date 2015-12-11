namespace Nest
{
	public abstract class ExponentialDecayFunction<TOrigin, TScale> : DecayFunctionBase<TOrigin, TScale>
	{
		protected override string DecayType => "exp";
	}

	public class ExponentialDecayFunctionDescriptor<TOrigin, TScale, T> : DecayFunctionBaseDescriptor<ExponentialDecayFunctionDescriptor<TOrigin, TScale, T>, TOrigin, TScale, T>
		where T : class
	{
		protected override string DecayType => "exp";
	}

	public class ExponentialDecayFunction : ExponentialDecayFunction<double?, double?> { }
	public class ExponentialDateDecayFunction : ExponentialDecayFunction<DateMath, TimeUnit> { }
	public class ExponentialGeoDecayFunction : ExponentialDecayFunction<GeoLocation, DistanceUnit> { }

}