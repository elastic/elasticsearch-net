using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	//CodeGeneration.LowLevelClient.Overrides.Descriptors.MultiTermVectorsDescriptorOverrides
	public class MultiTermVectorsDescriptorOverrides : IDescriptorOverrides
	{
		public IEnumerable<string> SkipQueryStringParams
		{
			get
			{
				return new string[]
				{
				};
			}
		}
	}
}
