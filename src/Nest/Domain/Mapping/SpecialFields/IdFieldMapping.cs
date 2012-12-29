using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
    public class IdFieldMapping
    {
		public IdFieldMapping()
        {
        }

        [JsonProperty("path")]
        public string Path { get; internal set; }

		[JsonProperty("index")]
		public string Index { get; internal set; }

		[JsonProperty("store"), JsonConverter(typeof(YesNoBoolConverter))]
		public bool? Store { get; internal set; }

		public IdFieldMapping SetPath(string path)
		{
			this.Path = path;
			return this;
		}
		public IdFieldMapping SetIndex(string index)
		{
			this.Index = index;
			return this;
		}
		public IdFieldMapping SetStored(bool stored = true)
		{
			this.Store = stored;
			return this;
		}
    }
}