using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiGenerator.Overrides.Descriptors
{
	// ReSharper disable once UnusedMember.Global
	public class MultiTermVectorsDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new []
		{
			"ids"
		};
	}
}
