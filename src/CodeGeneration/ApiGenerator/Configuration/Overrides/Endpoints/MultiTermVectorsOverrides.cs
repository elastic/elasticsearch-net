using System.Collections.Generic;

namespace ApiGenerator.Configuration.Overrides.Endpoints
{
	// ReSharper disable once UnusedMember.Global
	public class MultiTermVectorsOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"ids"
		};
	}
}
