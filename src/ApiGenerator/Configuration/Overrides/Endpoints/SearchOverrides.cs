// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace ApiGenerator.Configuration.Overrides.Endpoints
{
	// ReSharper disable once UnusedMember.Global
	public class SearchOverrides : EndpointOverridesBase
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
