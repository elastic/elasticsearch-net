using System;

namespace Nest
{
	public class TokenCountAttribute : NumberAttribute
	{
		public string Analyzer { get; set; }

		public override IElasticType ToElasticType() => new TokenCountType(this);
	}	
}
