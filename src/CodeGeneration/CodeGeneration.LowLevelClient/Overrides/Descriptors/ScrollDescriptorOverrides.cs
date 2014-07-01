using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	public class ScrollDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
				{
					"scroll_id", "scroll" 
				};
			}
		}

		public IDictionary<string, string> RenameQueryStringParams { get { return null; } }
	}
}
