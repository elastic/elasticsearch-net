using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	// ReSharper disable once UnusedMember.Global
	public class MultiTermVectorsOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new []
		{
			"ids"
		};
	}
}
