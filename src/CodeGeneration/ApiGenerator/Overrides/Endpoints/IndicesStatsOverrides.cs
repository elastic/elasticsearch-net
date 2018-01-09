using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	// ReSharper disable once UnusedMember.Global
	public class IndicesStatsOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new []
		{
			"types"
		};
	}
}
