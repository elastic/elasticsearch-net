using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	public class GetAnomalyRecordsDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"exclude_interim",
			"from",
			"size",
			"start",
			"end",
			"record_score",
			"sort",
			"desc"
		};
	}
}
