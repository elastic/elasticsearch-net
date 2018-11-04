using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	public class GetModelSnapshotsDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"from",
			"size",
			"start",
			"end",
			"sort",
			"desc"
		};
	}
}
