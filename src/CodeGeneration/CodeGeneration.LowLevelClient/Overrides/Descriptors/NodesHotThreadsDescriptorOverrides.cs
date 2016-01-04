using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	// ReSharper disable once UnusedMember.Global
	public class NodesHotThreadsDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams => new []
		{
			"fielddata"
		};

		public IDictionary<string, string> RenameQueryStringParams => new Dictionary<string, string>
		{
			{ "type", "thread_type"}
		};
	}
}
