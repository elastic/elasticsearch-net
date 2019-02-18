using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("clear_scroll.json")]
	public partial interface IClearScrollRequest
	{
		[JsonProperty("scroll_id")]
		IEnumerable<string> ScrollIds { get; set; }
	}

	public partial class ClearScrollRequest
	{
		public ClearScrollRequest(IEnumerable<string> scrollIds) => ScrollIds = scrollIds;

		public ClearScrollRequest(string scrollId) => ScrollIds = new string[] { scrollId };

		public IEnumerable<string> ScrollIds { get; set; }
	}

	public partial class ClearScrollDescriptor
	{
		IEnumerable<string> IClearScrollRequest.ScrollIds { get; set; }

		public ClearScrollDescriptor ScrollId(params string[] scrollIds) => Assign(a => a.ScrollIds = scrollIds);
	}
}
