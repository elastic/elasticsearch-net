using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.LowLevelClient.Overrides.Descriptors
{
	class FieldStatsDescriptorOverrides : IDescriptorOverrides
	{
		public IDictionary<string, string> RenameQueryStringParams => null;

		public IEnumerable<string> SkipQueryStringParams => new [] 
		{
			// Always send fields via the body since the endpoint doesn't 
			// allow a body at all if fields is in the query string.
			"fields"
		};
	}
}
