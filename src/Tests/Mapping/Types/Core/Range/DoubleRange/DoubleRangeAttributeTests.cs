using Nest;

namespace Tests.Mapping.Types.Core.Range.DoubleRange
{
	public class DoubleRangeTest
	{
		[DoubleRange]
		public Nest.DoubleRange Range { get; set; }
	}

	public class DoubleRangeAttributeTests : AttributeTestsBase<DoubleRangeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				range = new
				{
					type = "double_range"
				}
			}
		};
	}
}
