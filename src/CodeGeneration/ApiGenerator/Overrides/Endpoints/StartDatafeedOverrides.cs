using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class StartDatafeedOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"timeout",
			"start",
			"end",
		};
	}
}
