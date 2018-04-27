using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IIpRangeProperty : IRangeProperty { }

	public class IpRangeProperty : RangePropertyBase, IIpRangeProperty
	{
		public IpRangeProperty() : base(RangeType.IpRange) { }
	}

	public class IpRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<IpRangePropertyDescriptor<T>, IIpRangeProperty, T>, IIpRangeProperty
		where T : class
	{
		public IpRangePropertyDescriptor() : base(RangeType.IpRange) { }
	}
}
