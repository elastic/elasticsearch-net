using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Mapping.Types.Core.Range.IpRange
{
	public class IpRangeTest
	{
		[IpRange]
		public Nest.IpRange Range { get; set; }
	}

	[SkipVersion("<5.5.0", "ip range type is a new 5.5.0 feature")]
	public class IpRangeAttributeTests : AttributeTestsBase<IpRangeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				range = new
				{
					type = "ip_range"
				}
			}
		};
	}
}
