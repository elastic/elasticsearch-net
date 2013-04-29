using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
    public class ParentTypeMapping
    {
        [JsonProperty("type")]
		public TypeNameMarker Type { get; set; }
    }
}
