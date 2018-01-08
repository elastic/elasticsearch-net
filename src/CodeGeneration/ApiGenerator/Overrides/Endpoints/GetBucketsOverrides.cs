using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class GetBucketsOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"expand",
			"exclude_interim",
			"from",
			"size",
			"start",
			"end",
			"anomaly_score",
			"sort",
			"desc"
		};
	}
}
