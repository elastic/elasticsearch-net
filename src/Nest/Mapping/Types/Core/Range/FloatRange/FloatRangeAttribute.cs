using System;
using Elasticsearch.Net;

namespace Nest
{
	public class IntegerRangeAttribute : RangePropertyAttributeBase, IIntegerRangeProperty
	{
		public IntegerRangeAttribute() : base(RangeType.IntegerRange) { }
	}
}
