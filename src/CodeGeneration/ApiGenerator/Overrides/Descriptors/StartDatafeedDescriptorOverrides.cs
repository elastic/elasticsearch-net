using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	public class StartDatafeedDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"timeout",
			"start",
			"end",
		};
	}
}
