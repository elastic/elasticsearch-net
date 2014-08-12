using System;
using Newtonsoft.Json;

namespace Nest
{
    public class ParentFieldMapping : ISpecialField
    {
        [JsonProperty("type")]
		public TypeNameMarker Type { get; set; }
    }
	
	[Obsolete("Scheduled to be removed in 2.0, use ParentFieldMapping class instead")]
	public class ParentTypeMapping : ParentFieldMapping
	{
		
	}
}
