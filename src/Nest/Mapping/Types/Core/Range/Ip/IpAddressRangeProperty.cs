using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
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
