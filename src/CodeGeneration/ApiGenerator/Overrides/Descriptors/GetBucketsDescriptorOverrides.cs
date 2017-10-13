using System.Collections.Generic;
using System.Linq;
using ApiGenerator.Domain;

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
