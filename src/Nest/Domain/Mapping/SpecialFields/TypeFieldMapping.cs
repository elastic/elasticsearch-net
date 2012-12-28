using Newtonsoft.Json;

namespace Nest
{
    public class TypeFieldMapping
    {
		public TypeFieldMapping()
		{
			this.Index = true;
			this.Store = true;
		}

		[JsonProperty("index")]
		public bool Index { get; internal set; }

		[JsonProperty("store")]
		public bool Store { get; internal set; }

		public TypeFieldMapping SetIndexed(bool indexed = true)
		{
			this.Index = indexed;
			return this;
		}
		public TypeFieldMapping SetStored(bool stored = true)
		{
			this.Store = stored;
			return this;
		}
    }
}