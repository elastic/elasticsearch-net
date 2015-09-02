using System;

namespace Nest
{
	public class TokenCountAttribute : NumberAttribute
	{
		public string Analyzer { get; set; }

		public override IProperty ToProperty() => new TokenCountProperty(this);
	}	
}
