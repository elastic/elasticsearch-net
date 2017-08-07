using System.Collections.Generic;
using System.Linq;
using ApiGenerator.Domain;

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
