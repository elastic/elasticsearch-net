using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A range of double-precision 64-bit IEEE 754 floating point values.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public interface IDoubleRangeProperty : IRangeProperty { }

	/// <inheritdoc/>
	public class DoubleRangeProperty : RangePropertyBase, IDoubleRangeProperty
	{
		public DoubleRangeProperty() : base(RangeType.DoubleRange) { }
	}

	/// <inheritdoc/>
	public class DoubleRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<DoubleRangePropertyDescriptor<T>, IDoubleRangeProperty, T>, IDoubleRangeProperty
		where T : class
	{
		public DoubleRangePropertyDescriptor() : base(RangeType.DoubleRange) { }
	}
}
