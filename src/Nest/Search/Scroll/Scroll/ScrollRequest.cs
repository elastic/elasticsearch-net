using Newtonsoft.Json;

namespace Nest
{
	public partial interface IScrollRequest 
	{
		[JsonProperty("scroll")]
		Time Scroll { get; set; }

		[JsonProperty("scroll_id")]
		string ScrollId { get; set; }
	}
	
	public partial class ScrollRequest 
	{
		public Time Scroll { get; set; }

		public string ScrollId { get; set; }

		public ScrollRequest(string scrollId, Time scroll)
		{
			this.Scroll = scroll;
			this.ScrollId = scrollId;
		}
	}

	public partial class ScrollDescriptor<T> where T : class
	{
		Time IScrollRequest.Scroll { get; set; }

		string IScrollRequest.ScrollId { get; set; }

		///<summary>Specify how long a consistent view of the index should be maintained for scrolled search</summary>
		public ScrollDescriptor<T> Scroll(Time scroll) => Assign(a => a.Scroll = scroll);

		public ScrollDescriptor<T> ScrollId(string scrollId) => Assign(a => a.ScrollId = scrollId);
	}
}
