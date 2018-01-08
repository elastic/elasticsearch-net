using System.Collections.Generic;

namespace ApiGenerator.Overrides.Endpoints
{
	// ReSharper disable once UnusedMember.Global
	public class ClearCacheOverrides : EndpointOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new []
		{
			"field_data" //this API declares both field_data and fielddata, this is the odd one out.
		};
	}
}
