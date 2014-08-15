using System;
using Newtonsoft.Json;

namespace Nest
{
    public class ParentFieldMapping : ISpecialField
    {
        [JsonProperty("type")]
		public TypeNameMarker Type { get; set; }
    }
}
