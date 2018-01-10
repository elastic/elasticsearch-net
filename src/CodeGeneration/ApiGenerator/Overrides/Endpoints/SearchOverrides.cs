using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	// ReSharper disable once UnusedMember.Global
	public class SearchOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new []
		{
			"size",
			"from",
			"timeout",
			"explain",
			"version",
			"q", //we dont support GET searches
			"sort",
			"_source",
			"_source_include",
			"_source_exclude",
			"track_scores",
			"terminate_after",
		};
	}
}
