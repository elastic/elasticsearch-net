using System;
using Elasticsearch.Net_5_2_0;

namespace Nest_5_2_0
{
	public class LongRangeAttribute : RangePropertyAttributeBase, ILongRangeProperty
	{
		public LongRangeAttribute() : base(RangeType.LongRange) { }
	}
}
