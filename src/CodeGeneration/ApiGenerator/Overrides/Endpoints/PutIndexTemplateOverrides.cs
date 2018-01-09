using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	// ReSharper disable once UnusedMember.Global
	public class PutIndexTemplateOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new []
		{
			"order"
		};
	}
}
