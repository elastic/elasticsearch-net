using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class UpdateModelSnapshotOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"description",
			"retain"
		};
	}
}
