using System;

namespace Nest
{
	public class BooleanAttribute : ElasticPropertyAttribute 
	{
		public NonStringIndexOption? Index { get; set; }
		public double Boost { get; set; }
		public bool NullValue { get; set; }

		public override IElasticType ToElasticType() => new BooleanType(this);
	}	
}
