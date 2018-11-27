using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A range of double-precision 64-bit IEEE 754 floating point values.
	/// </summary>
	[DataContract]
	public interface IDoubleRangeProperty : IRangeProperty { }

	/// <inheritdoc />
	public class DoubleRangeProperty : RangePropertyBase, IDoubleRangeProperty
	{
		public DoubleRangeProperty() : base(RangeType.DoubleRange) { }
	}

	/// <inheritdoc />
	public class DoubleRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<DoubleRangePropertyDescriptor<T>, IDoubleRangeProperty, T>, IDoubleRangeProperty
		where T : class
	{
		public DoubleRangePropertyDescriptor() : base(RangeType.DoubleRange) { }
	}
}
