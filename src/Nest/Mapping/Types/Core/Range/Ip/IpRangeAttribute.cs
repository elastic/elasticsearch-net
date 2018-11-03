namespace Nest
{
	public class IpRangeAttribute : RangePropertyAttributeBase, IIpRangeProperty
	{
		public IpRangeAttribute() : base(RangeType.IpRange) { }
	}
}
