using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class StopDatafeedOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"timeout",
			"force"
		};
	}
}
