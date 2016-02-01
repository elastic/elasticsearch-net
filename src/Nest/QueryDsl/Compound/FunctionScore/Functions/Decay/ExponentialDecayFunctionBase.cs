namespace Nest
{
	public abstract class ExponentialDecayFunctionBase<TOrigin, TScale> : DecayFunctionBase<TOrigin, TScale>
	{
		protected override string DecayType => "exp";
	}

	public class ExponentialDecayFunctionDescriptor<TOrigin, TScale, T> : DecayFunctionDescriptorBase<ExponentialDecayFunctionDescriptor<TOrigin, TScale, T>, TOrigin, TScale, T>
		where T : class
	{
		protected override string DecayType => "exp";
	}

	public class ExponentialDecayFunction : ExponentialDecayFunctionBase<double?, double?> { }
	public class ExponentialDateDecayFunction : ExponentialDecayFunctionBase<DateMath, Time> { }
	public class ExponentialGeoDecayFunction : ExponentialDecayFunctionBase<GeoLocation, Distance> { }

}