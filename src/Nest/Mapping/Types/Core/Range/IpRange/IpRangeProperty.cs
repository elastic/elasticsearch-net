// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

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
