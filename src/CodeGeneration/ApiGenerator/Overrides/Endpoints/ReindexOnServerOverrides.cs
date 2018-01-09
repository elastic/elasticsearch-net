using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	// ReSharper disable once UnusedMember.Global
	public class ReindexOnServerOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new []
		{
			"source"
		};
	}

}
