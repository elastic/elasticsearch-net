using Nest.Resolvers.Converters;
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

		[JsonProperty("index"), JsonConverter(typeof(YesNoBoolConverter))]
		public bool? Index { get; internal set; }

		[JsonProperty("store"), JsonConverter(typeof(YesNoBoolConverter))]
		public bool? Store { get; internal set; }

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