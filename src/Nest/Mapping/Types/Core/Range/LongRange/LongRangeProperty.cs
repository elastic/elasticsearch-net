using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
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
