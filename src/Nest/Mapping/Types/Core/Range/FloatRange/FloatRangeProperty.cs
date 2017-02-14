using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A range of single-precision 32-bit IEEE 754 floating point values.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public interface IFloatRangeProperty : IRangeProperty { }

	/// <inheritdoc/>
	public class FloatRangeProperty : RangePropertyBase, IFloatRangeProperty
	{
		public FloatRangeProperty() : base(RangeType.FloatRange) { }
	}

	/// <inheritdoc/>
	public class FloatRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<FloatRangePropertyDescriptor<T>, IFloatRangeProperty, T>, IFloatRangeProperty
		where T : class
	{
		public FloatRangePropertyDescriptor() : base(RangeType.FloatRange) { }
	}
}
