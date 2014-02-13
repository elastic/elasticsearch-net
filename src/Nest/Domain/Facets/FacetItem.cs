using Newtonsoft.Json;

namespace Nest
{
	public abstract class FacetItem
	{
		[JsonProperty("count")]
		public virtual long Count { get; internal set; }
	}
}