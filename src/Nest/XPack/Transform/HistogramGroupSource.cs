namespace Nest
{
	/// <summary>
	/// The histogram value source can be applied on numeric values to build fixed size interval over the values.
	/// </summary>
	public interface IHistogramGroupSource : ISingleGroupSource { }

	/// <inheritdoc cref="IHistogramGroupSource"/>
	public class HistogramGroupSource : SingleGroupSourceBase, IHistogramGroupSource
	{
		public HistogramGroupSource(string name) : base(name) { }

		protected override string Type => "histogram";
	}

	/// <inheritdoc cref="IHistogramGroupSource"/>
	public class HistogramGroupSourceDescriptor<T>
		: SingleGroupSourceDescriptorBase<HistogramGroupSourceDescriptor<T>, IHistogramGroupSource, T>,
			IHistogramGroupSource
	{
		public HistogramGroupSourceDescriptor(string name) : base(name, "histogram") { }
	}
}
