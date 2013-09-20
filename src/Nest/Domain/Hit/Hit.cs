using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ConcreteTypeConverter))]
    public interface IHit<out T> where T : class
    {
        T Fields { get; }
        T Source { get; }
        string Index { get; }
        double Score { get; }
        string Type { get; }
        string Version { get; }
        string Id { get; }

        IEnumerable<object> Sorts { get; }

		HighlightFieldDictionary Highlights { get; }
        Explanation Explanation { get; }
        ICovariantDictionary<T> PartialFields { get; }
    }

    [JsonObject]
    public class Hit<T> : IHit<T>
        where T : class
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
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, List<string>> _Highlight { get; set; }

		public HighlightFieldDictionary Highlights
		{
			get
			{
				if (_Highlight == null)
					return new HighlightFieldDictionary();

				var highlights = _Highlight.Select(kv => new Highlight
				{
					DocumentId = this.Id,
					Field = kv.Key,
					Highlights = kv.Value
				}).ToDictionary(k=>k.Field, v=>v);

				return new HighlightFieldDictionary(highlights);
			} 
		}

		[JsonProperty(PropertyName = "_explanation")]
		public Explanation Explanation { get; internal set; }

        
        public ICovariantDictionary<T> PartialFields { get; internal set; }

        public Hit()
        {

        }
    }
}
