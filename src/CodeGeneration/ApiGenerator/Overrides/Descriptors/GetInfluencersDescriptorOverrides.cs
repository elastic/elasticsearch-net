using System.Collections.Generic;
using System.Linq;
using ApiGenerator.Domain;

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
