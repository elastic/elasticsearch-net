using System.Collections.Generic;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	// ReSharper disable once UnusedMember.Global
	public class UpdateDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new []
		{
			"fields"
		};
	}
}
