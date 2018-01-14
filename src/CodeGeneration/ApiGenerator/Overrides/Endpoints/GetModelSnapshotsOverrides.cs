using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class GetModelSnapshotsOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"from",
			"size",
			"start",
			"end",
			"sort",
			"desc"
		};
	}
}
