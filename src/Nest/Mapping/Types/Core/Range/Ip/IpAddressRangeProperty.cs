using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IIpAddressRangeProperty : IRangeProperty { }

	public class IpAddressRangeProperty : RangePropertyBase, IIpAddressRangeProperty
	{
		public IpAddressRangeProperty() : base(RangeType.IpRange) { }
	}

	public class IpAddressRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<IpAddressRangePropertyDescriptor<T>, IIpAddressRangeProperty, T>, IIpAddressRangeProperty
		where T : class
	{
		public IpAddressRangePropertyDescriptor() : base(RangeType.IpRange) { }
	}
}
