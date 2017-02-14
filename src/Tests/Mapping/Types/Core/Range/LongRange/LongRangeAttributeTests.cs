using Nest;

namespace Tests.Mapping.Types.Core.Range.LongRange
{
	public class LongRangeTest
	{
		[LongRange]
		public Nest.LongRange Range { get; set; }
	}

	public class LongRangeAttributeTests : AttributeTestsBase<LongRangeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				range = new
				{
					type = "long_range"
				}
			}
		};
	}
}
