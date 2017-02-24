using System;
using Elasticsearch.Net_5_2_0;

namespace Nest_5_2_0
{
	public class DoubleRangeAttribute : RangePropertyAttributeBase, IDoubleRangeProperty
	{
		public DoubleRangeAttribute() : base(RangeType.DoubleRange) { }
	}
}
