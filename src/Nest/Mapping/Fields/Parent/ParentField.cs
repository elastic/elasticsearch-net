using System;
using Newtonsoft.Json;

namespace Nest
{
    public class ParentField : ISpecialField
    {
        [JsonProperty("type")]
		public TypeName Type { get; set; }
    }
}
