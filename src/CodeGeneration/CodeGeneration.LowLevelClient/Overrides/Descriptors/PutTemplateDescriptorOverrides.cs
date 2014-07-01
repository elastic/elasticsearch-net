using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	public class PutTemplateDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
				{
					"order" 
				};
			}
		}
		public IDictionary<string, string> RenameQueryStringParams { get { return null; } }
	}
	
	public class UpdateDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
				{
					"fields" 
				};
			}
		}
	}
}
