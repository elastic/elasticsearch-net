﻿namespace Nest
{
	public class FloatRangeAttribute : RangePropertyAttributeBase, IFloatRangeProperty
	{
		public FloatRangeAttribute() : base(RangeType.FloatRange) { }
	}
}
