using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
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
