using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class Hit<T> where T : class
	{
		[JsonProperty(PropertyName = "fields")]
		public T Fields { get; internal set; }
		[JsonProperty(PropertyName = "_source")]
		public T Source { get; internal set; }
		[JsonProperty(PropertyName = "_index")]
		public string Index { get; internal set; }
		[JsonProperty(PropertyName = "_score")]
        public double Score { get; internal set; }
		[JsonProperty(PropertyName = "_type")]
		public string Type { get; internal set; }
		[JsonProperty(PropertyName = "_version")]
		public string Version { get; internal set; }
		[JsonProperty(PropertyName = "_id")]
		public string Id { get; internal set; }

		[JsonProperty(PropertyName = "sort")]
		public IEnumerable<object> Sorts { get; internal set; }

		[JsonProperty(PropertyName = "highlight")]
		public Dictionary<string, List<string>> Highlight { get; internal set; }
		[JsonProperty(PropertyName = "_explanation")]
		public Explanation Explanation { get; internal set; }
	}
}
