using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	public class NodesHotThreadsDescriptorOverrides : DescriptorOverridesBase
	{
		public override IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
				{
					"fielddata"
				};
			}
		}

		public override IDictionary<string, string> RenameQueryStringParams
		{
			get
			{
				return new Dictionary<string, string>
				{
					{ "type", "thread_type"}
				};
			}
		}
	}
}
