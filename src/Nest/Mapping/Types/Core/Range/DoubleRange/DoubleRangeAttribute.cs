using System;
using Elasticsearch.Net;

namespace Nest
{
	public class DoubleRangeAttribute : RangePropertyAttributeBase, IDoubleRangeProperty
	{
		public DoubleRangeAttribute() : base(RangeType.DoubleRange) { }
	}
}
