using System;

namespace Nest
{
	public class NumberAttribute : ElasticPropertyAttribute
	{
		public NumberAttribute(NumberTypeName numberType)
		{
			NumberType = numberType;
		}

		public NumberAttribute()
		{
			NumberType = NumberTypeName.Double;
		}

		public NumberTypeName NumberType { get; set; }
		public NonStringIndexOption? Index { get; set; }
		public double Boost { get; set; }
		public double NullValue { get; set; }
		public bool IncludeInAll { get; set; }
		public int PrecisionStep { get; set; }
		public bool IgnoreMalformed { get; set; }
		public bool Coerce { get; set; }

		public override IElasticType ToElasticType() => new NumberType(this);
	}
}
