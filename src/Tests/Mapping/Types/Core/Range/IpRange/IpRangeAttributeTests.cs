using Nest;
using Tests.Framework;

namespace Tests.Mapping.Types.Core.Range.IpRange
{
	public class IpRangeTest
	{
		[IpAddressRange]
		public Nest.IpAddressRange Range { get; set; }
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
