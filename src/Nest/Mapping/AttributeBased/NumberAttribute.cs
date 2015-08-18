using System;

namespace Nest
{
	public class NumberAttribute : ElasticPropertyAttribute
	{
		public NumberAttribute(NumberType type)
		{
			Type = type;
		}

		public NumberAttribute()
		{
			Type = NumberType.Double;
		}

		public NumberType Type { get; set; }
		public NonStringIndexOption? Index { get; set; }
		public double Boost { get; set; }
		public double NullValue { get; set; }
		public bool IncludeInAll { get; set; }
		public int PrecisionStep { get; set; }
		public bool IgnoreMalformed { get; set; }
		public bool Coerce { get; set; }

		public override IElasticsearchProperty ToProperty() => new NumberProperty(this);
	}
}
