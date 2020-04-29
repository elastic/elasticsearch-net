namespace Nest
{
	public interface IDateHistogramGroupSource : ISingleGroupSource { }

	public class DateHistogramGroupSource : SingleGroupSourceBase, IDateHistogramGroupSource
	{
		public DateHistogramGroupSource(string name) : base(name) { }

		protected override string Type => "date_histogram";
	}

	public class DateHistogramGroupSourceDescriptor<T>
		: SingleGroupSourceDescriptorBase<DateHistogramGroupSourceDescriptor<T>, IDateHistogramGroupSource, T>,
			IDateHistogramGroupSource
	{
		public DateHistogramGroupSourceDescriptor(string name) : base(name, "date_histogram") { }
	}
}
