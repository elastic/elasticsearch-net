using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
    public class TypeFieldMapping
    {
		[JsonProperty("index"), JsonConverter(typeof(StringEnumConverter))]
		public NonStringIndexOption? Index { get; internal set; }

		[JsonProperty("store"), JsonConverter(typeof(YesNoBoolConverter))]
		public bool? Store { get; internal set; }

		public TypeFieldMapping SetIndexed(NonStringIndexOption index = NonStringIndexOption.analyzed)
		{
			this.Index = index;
			return this;
		}
		public TypeFieldMapping SetStored(bool stored = true)
		{
			this.Store = stored;
			return this;
		}
    }
}