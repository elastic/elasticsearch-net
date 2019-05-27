using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
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
