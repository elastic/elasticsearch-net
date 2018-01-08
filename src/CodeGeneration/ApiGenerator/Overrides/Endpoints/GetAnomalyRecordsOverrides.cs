using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class GetAnomalyRecordsOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"exclude_interim",
			"from",
			"size",
			"start",
			"end",
			"record_score",
			"sort",
			"desc"
		};
	}
}
