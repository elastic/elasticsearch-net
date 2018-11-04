using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	public class GetBucketsDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"expand",
			"exclude_interim",
			"from",
			"size",
			"start",
			"end",
			"anomaly_score",
			"sort",
			"desc"
		};
	}
}
