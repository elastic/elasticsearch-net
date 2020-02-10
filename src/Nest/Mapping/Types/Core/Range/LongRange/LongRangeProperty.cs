using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A range of signed 64-bit integers with a minimum value of -263 and maximum of 263-1.
	/// </summary>
	[InterfaceDataContract]
	public interface ILongRangeProperty : IRangeProperty { }

	/// <inheritdoc cref="ILongRangeProperty"/>
	public class LongRangeProperty : RangePropertyBase, ILongRangeProperty
	{
		public LongRangeProperty() : base(RangeType.LongRange) { }
	}

	/// <inheritdoc cref="ILongRangeProperty"/>
	public class LongRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<LongRangePropertyDescriptor<T>, ILongRangeProperty, T>, ILongRangeProperty
		where T : class
	{
		public LongRangePropertyDescriptor() : base(RangeType.LongRange) { }
	}
}
