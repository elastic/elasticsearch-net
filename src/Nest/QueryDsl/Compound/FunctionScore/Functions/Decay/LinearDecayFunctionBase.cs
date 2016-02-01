namespace Nest
{
	public abstract class LinearDecayFunctionBase<TOrigin, TScale> : DecayFunctionBase<TOrigin, TScale>
	{
		protected override string DecayType => "linear";
	}
	public class LinearDecayFunctionDescriptor<TOrigin, TScale, T> : DecayFunctionDescriptorBase<LinearDecayFunctionDescriptor<TOrigin, TScale, T>, TOrigin, TScale, T>
		where T : class
	{
		protected override string DecayType => "linear";
	}

	public class LinearDecayFunction : LinearDecayFunctionBase<double?, double?> { }
	public class LinearDateDecayFunction : LinearDecayFunctionBase<DateMath, Time> { }
	public class LinearGeoDecayFunction : LinearDecayFunctionBase<GeoLocation, Distance> { }
}