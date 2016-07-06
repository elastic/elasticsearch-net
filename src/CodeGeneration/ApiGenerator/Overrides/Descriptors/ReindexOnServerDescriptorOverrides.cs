using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	// ReSharper disable once UnusedMember.Global
	public class ReindexOnServerDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new []
		{
			"source"
		};
	}

}
