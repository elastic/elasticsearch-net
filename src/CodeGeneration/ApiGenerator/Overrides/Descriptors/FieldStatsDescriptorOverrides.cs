using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	internal class FieldStatsDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			// Always send fields via the body since the endpoint doesn't
			// allow a body at all if fields is in the query string.
			"fields"
		};
	}
}
