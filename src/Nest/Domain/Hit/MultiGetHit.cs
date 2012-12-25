using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject]
    public class MultiGetHit<T> where T : class
    {
		[JsonProperty(PropertyName = "fields")]
		public T Fields { get; internal set; }
		
		[JsonProperty(PropertyName = "_source")]
		public T Source { get; internal set; }
		
		[JsonProperty(PropertyName = "_index")]
		public string Index { get; internal set; }
		
		[JsonProperty(PropertyName = "exists")]
		public bool Exists{ get; internal set; }
		
		[JsonProperty(PropertyName = "_type")]
		public string Type { get; internal set; }
		
		[JsonProperty(PropertyName = "_version")]
		public string Version { get; internal set; }
		
		[JsonProperty(PropertyName = "_id")]
		public string Id { get; internal set; }
    }
}
