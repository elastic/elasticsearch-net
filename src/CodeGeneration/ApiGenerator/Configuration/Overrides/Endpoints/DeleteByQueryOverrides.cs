using System.Collections.Generic;

namespace ApiGenerator.Configuration.Overrides.Endpoints
{
	public class DeleteByQueryOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[] { "max_docs", };
	}
}
