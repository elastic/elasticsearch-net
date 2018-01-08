using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class FieldStatsOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new []
		{
			// Always send fields via the body since the endpoint doesn't
			// allow a body at all if fields is in the query string.
			"fields"
		};
	}
}
