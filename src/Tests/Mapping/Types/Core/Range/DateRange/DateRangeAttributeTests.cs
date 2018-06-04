using System.Reflection;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.Mapping.Types.Core.Range.DateRange
{
	public class DateRangeTest
	{
		[DateRange(Boost=1.2, Coerce = false, Format = "yyyy-MM")]
		public Nest.DateRange Range { get; set; }
	}

	[SkipVersion("<5.2.0", "dedicated range types is a new 5.2.0 feature")]
	public class DateRangeAttributeTests : AttributeTestsBase<DateRangeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				range = new
				{
					boost = 1.2,
					coerce = false,
					format = "yyyy-MM",
					type = "date_range"
				}
			}
		};
	}
}
