using System;
using Elasticsearch.Net_5_2_0;

namespace Nest_5_2_0
{
	public class IntegerRangeAttribute : RangePropertyAttributeBase, IIntegerRangeProperty
	{
		public IntegerRangeAttribute() : base(RangeType.IntegerRange) { }
	}
}
