using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class ForecastJobOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"duration",
			"expires_in"
		};
	}
}