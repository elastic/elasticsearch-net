using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	public class GetInfluencersDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"exclude_interim",
			"from",
			"size",
			"start",
			"end",
			"influencer_score",
			"sort",
			"desc"
		};
	}
}
