using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	public class StopDatafeedDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"timeout",
			"force"
		};
	}
}
