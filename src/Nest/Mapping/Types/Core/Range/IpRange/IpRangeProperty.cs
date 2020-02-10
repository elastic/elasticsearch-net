using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A range of ip values supporting either IPv4 or IPv6 (or mixed) addresses.
	/// </summary>
	[InterfaceDataContract]
	public interface IIpRangeProperty : IRangeProperty { }

	/// <inheritdoc cref="IIpRangeProperty"/>
	public class IpRangeProperty : RangePropertyBase, IIpRangeProperty
	{
		public IpRangeProperty() : base(RangeType.IpRange) { }
	}

	/// <inheritdoc cref="IIpRangeProperty"/>
	public class IpRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<IpRangePropertyDescriptor<T>, IIpRangeProperty, T>, IIpRangeProperty
		where T : class
	{
		public IpRangePropertyDescriptor() : base(RangeType.IpRange) { }
	}
}
