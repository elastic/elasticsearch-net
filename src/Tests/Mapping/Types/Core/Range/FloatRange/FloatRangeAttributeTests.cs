using Nest;

namespace Tests.Mapping.Types.Core.Range.FloatRange
{
	public class FloatRangeTest
	{
		[FloatRange]
		public Nest.FloatRange Range { get; set; }
	}

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
