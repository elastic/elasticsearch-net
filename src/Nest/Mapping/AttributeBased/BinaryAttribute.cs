using System;

namespace Nest
{
	public class BinaryAttribute : ElasticPropertyAttribute
	{
		public override IElasticType ToElasticType() => new BinaryType(this);
	}	
}
