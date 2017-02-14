using Nest;

namespace Tests.Mapping.Types.Core.Range.IntegerRange
{
	public class IntegerRangeTest
	{
		[IntegerRange]
		public Nest.IntegerRange Range { get; set; }
	}

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
