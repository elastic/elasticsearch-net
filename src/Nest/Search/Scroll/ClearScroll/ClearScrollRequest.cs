using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public partial interface IClearScrollRequest
	{
		[JsonProperty("scroll_id")]
		IEnumerable<string> ScrollIds { get; set; }
	}

	public partial class ClearScrollRequest
	{
		public IEnumerable<string> ScrollIds { get; set; }

		public ClearScrollRequest(IEnumerable<string> scrollIds)
		{
			this.ScrollIds = scrollIds;
		}

		public ClearScrollRequest(string scrollId)
		{
			this.ScrollIds = new string[] { scrollId };
		}
	}

	[DescriptorFor("ClearScroll")]
	public partial class ClearScrollDescriptor
	{
		IEnumerable<string> IClearScrollRequest.ScrollIds { get; set; }

		public ClearScrollDescriptor ScrollId(params string[] scrollIds) => Assign(a => a.ScrollIds = scrollIds);
	}
}
