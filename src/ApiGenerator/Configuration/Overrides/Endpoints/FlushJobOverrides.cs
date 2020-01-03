using System.Collections.Generic;

namespace ApiGenerator.Configuration.Overrides.Endpoints
{
	public class FlushJobOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"advance_time",
			"end",
			"start",
			"calc_interim",
		};
	}
}
