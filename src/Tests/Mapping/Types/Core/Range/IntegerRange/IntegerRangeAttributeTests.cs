using Nest_5_2_0;
using Tests.Framework;

namespace Tests.Mapping.Types.Core.Range.IntegerRange
{
	public class IntegerRangeTest
	{
		[IntegerRange]
		public Nest_5_2_0.IntegerRange Range { get; set; }
	}

	[SkipVersion("<5.2.0", "dedicated range types is a new 5.2.0 feature")]
	public class IntegerRangeAttributeTests : AttributeTestsBase<IntegerRangeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				range = new
				{
					type = "integer_range"
				}
			}
		};
	}
}
