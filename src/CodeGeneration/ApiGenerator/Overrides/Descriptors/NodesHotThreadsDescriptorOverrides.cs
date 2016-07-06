using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiGenerator.Overrides.Descriptors
{
	// ReSharper disable once UnusedMember.Global
	public class NodesHotThreadsDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams => new []
		{
			"fielddata"
		};

		public override IDictionary<string, string> RenameQueryStringParams => new Dictionary<string, string>
		{
			{ "type", "thread_type"}
		};
	}
}
