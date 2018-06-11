using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.Mapping.Types.Core.Range.FloatRange
{
	public class FloatRangeTest
	{
		[FloatRange]
		public Nest.FloatRange Range { get; set; }
	}

	[SkipVersion("<5.2.0", "dedicated range types is a new 5.2.0 feature")]
	public class FloatRangeAttributeTests : AttributeTestsBase<FloatRangeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				range = new
				{
					type = "float_range"
				}
			}
		};
	}
}
