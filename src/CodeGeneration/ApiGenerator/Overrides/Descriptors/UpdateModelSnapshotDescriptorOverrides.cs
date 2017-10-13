using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	public class UpdateModelSnapshotDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"description",
			"retain"
		};
	}
}
