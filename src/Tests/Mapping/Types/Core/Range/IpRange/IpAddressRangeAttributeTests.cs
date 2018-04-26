using Nest;
using Tests.Framework;

namespace Tests.Mapping.Types.Core.Range.IpRange
{
	public class IpAddressRangeTest
	{
		[IpAddressRange]
		public Nest.IpAddressRange Range { get; set; }
	}

	[SkipVersion("<5.5.0", "ip range type is a new 5.5.0 feature")]
	public class IpAddressRangeAttributeTests : AttributeTestsBase<IpAddressRangeTest>
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
