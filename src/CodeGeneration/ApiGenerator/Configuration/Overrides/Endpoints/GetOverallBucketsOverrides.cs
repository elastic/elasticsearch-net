using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class GetOverallBucketsOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"allow_no_jobs",
			"bucket_span",
			"end",
			"exclude_interim",
			"overall_score",
			"start",
			"top_n"
		};
	}
}
