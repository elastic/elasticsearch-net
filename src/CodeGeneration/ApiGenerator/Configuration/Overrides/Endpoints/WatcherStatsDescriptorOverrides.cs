using System.Collections.Generic;

namespace ApiGenerator.Configuration.Overrides.Endpoints
{
	public class WatcherStatsOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			// this is already included in the url route
			"metric"
		};
	}
}
