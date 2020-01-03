using System.Collections.Generic;

namespace ApiGenerator.Configuration.Overrides.Endpoints
{
	// ReSharper disable once UnusedMember.Global
	public class ScrollOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"scroll_id", "scroll"
		};
	}
}
