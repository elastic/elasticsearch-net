using System.Collections.Generic;

namespace ApiGenerator.Configuration.Overrides.Endpoints
{
	// ReSharper disable once UnusedMember.Global
	public class AsyncSearchSubmitOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"size",
			"from",
			"timeout",
			"explain",
			"version",
			"sort",
			"_source",
			"_source_includes",
			"_source_excludes",
			"track_scores",
			"terminate_after",
		};

		public override IEnumerable<string> RenderPartial => new[]
		{
			"track_total_hits"
		};
	}
}
