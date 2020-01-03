using System.Collections.Generic;

namespace ApiGenerator.Configuration.Overrides.Endpoints
{
	public class GetCalendarEventsOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"from",
			"size"
		};
	}
}
