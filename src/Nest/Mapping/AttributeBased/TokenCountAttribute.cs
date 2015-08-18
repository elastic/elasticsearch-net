using System;

namespace Nest
{
	public class TokenCountAttribute : NumberAttribute
	{
		public string Analyzer { get; set; }

		public override IElasticsearchProperty ToProperty() => new TokenCountProperty(this);
	}	
}
