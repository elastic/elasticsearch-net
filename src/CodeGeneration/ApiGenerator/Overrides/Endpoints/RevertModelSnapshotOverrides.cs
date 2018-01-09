using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class RevertModelSnapshotOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"delete_intervening_results"
		};
	}
}
