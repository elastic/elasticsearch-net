using System;
using Elasticsearch.Net;

namespace Nest
{
	public class IpAddressRangeAttribute : RangePropertyAttributeBase, IIpAddressRangeProperty
	{
		public IpAddressRangeAttribute() : base(RangeType.IpRange) { }
	}
}
