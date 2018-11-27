using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface ILongRangeProperty : IRangeProperty { }

	public class LongRangeProperty : RangePropertyBase, ILongRangeProperty
	{
		public LongRangeProperty() : base(RangeType.LongRange) { }
	}

	public class LongRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<LongRangePropertyDescriptor<T>, ILongRangeProperty, T>, ILongRangeProperty
		where T : class
	{
		public LongRangePropertyDescriptor() : base(RangeType.LongRange) { }
	}
}
