using System;
using Elasticsearch.Net;

namespace Nest
{
	public class LongRangeAttribute : RangePropertyAttributeBase, ILongRangeProperty
	{
		public LongRangeAttribute() : base(RangeType.LongRange) { }
	}
}
