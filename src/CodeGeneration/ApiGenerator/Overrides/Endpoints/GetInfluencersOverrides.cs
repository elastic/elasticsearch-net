using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class GetInfluencersOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"exclude_interim",
			"from",
			"size",
			"start",
			"end",
			"influencer_score",
			"sort",
			"desc"
		};
	}
}
