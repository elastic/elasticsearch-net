using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	public class FlushJobDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"advance_time",
			"end",
			"start",
			"calc_interim",
		};
	}
}
