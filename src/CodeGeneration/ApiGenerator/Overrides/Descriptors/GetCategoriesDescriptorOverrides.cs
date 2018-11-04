using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	public class GetCategoriesDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"from",
			"size"
		};
	}
}
