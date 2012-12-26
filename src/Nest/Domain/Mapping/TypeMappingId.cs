using Newtonsoft.Json;

namespace Nest
{
    public class IdMapping
    {
		public IdMapping()
        {
        }

        [JsonProperty("path")]
        public string Path { get; internal set; }

		[JsonProperty("index")]
		public string Index { get; internal set; }

		[JsonProperty("store")]
		public bool Store { get; internal set; }

		public IdMapping SetPath(string path)
		{
			this.Path = path;
			return this;
		}
		public IdMapping SetIndex(string index)
		{
			this.Index = index;
			return this;
		}
		public IdMapping SetStored(bool stored = true)
		{
			this.Store = stored;
			return this;
		}
    }
}