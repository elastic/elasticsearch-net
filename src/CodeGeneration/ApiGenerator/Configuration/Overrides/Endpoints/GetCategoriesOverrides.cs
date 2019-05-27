using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	public class GetCategoriesOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"from",
			"size"
		};
	}
}
