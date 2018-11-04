using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	public class RevertModelSnapshotDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"delete_intervening_results"
		};
	}
}
