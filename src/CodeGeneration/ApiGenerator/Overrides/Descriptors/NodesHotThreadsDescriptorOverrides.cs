using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	// ReSharper disable once UnusedMember.Global
	public class NodesHotThreadsDescriptorOverrides : DescriptorOverridesBase
	{
		public override IDictionary<string, string> RenameQueryStringParams => new Dictionary<string, string>
		{
			{ "type", "thread_type" }
		};

		public override IEnumerable<string> SkipQueryStringParams => new[]
		{
			"fielddata"
		};
	}
}
