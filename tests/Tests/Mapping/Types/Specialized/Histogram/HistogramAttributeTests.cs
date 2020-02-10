using Nest;

namespace Tests.Mapping.Types.Specialized.Histogram
{
	public class HistogramTest
	{
		[Histogram(IgnoreMalformed = true)]
		public string Full { get; set; }

		[Histogram]
		public string Minimal { get; set; }
	}

	public class HistogramAttributeTests : AttributeTestsBase<HistogramTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "histogram",
					ignore_malformed = true
				},
				minimal = new
				{
					type = "histogram"
				}
			}
		};
	}
}
