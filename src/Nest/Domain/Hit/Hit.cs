using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Domain;
using Newtonsoft.Json;

namespace Nest
{
	//[JsonConverter(typeof(ConcreteTypeConverter))]
	public interface IHit<out T> where T : class
	{
		IFieldSelection<T> Fields { get; }
		T Source { get; }
		string Index { get; }
		double Score { get; }
		string Type { get; }
		string Version { get; }
		string Id { get; }
		IEnumerable<object> Sorts { get; }
		HighlightFieldDictionary Highlights { get; }
		Explanation Explanation { get; }
		IEnumerable<string> MatchedQueries { get; }
	}

	[JsonObject]
	public class Hit<T> : IHit<T>
		where T : class
	{
		[JsonProperty(PropertyName = "fields")]
		internal IDictionary<string, object> _fields { get; set; }
		[JsonIgnore]
		public IFieldSelection<T> Fields { get; internal set; }
		[JsonProperty(PropertyName = "_source")]
		public T Source { get; internal set; }
		[JsonProperty(PropertyName = "_index")]
		public string Index { get; internal set; }
		
		//TODO in NEST 2.0 make the property itself double?
		[JsonProperty(PropertyName = "_score")]
		internal double? _score { get; set; }
		public double Score { get { return _score.GetValueOrDefault(0); } }

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
				}).ToDictionary(k => k.Field, v => v);

				return new HighlightFieldDictionary(highlights);
			}
		}

		[JsonProperty(PropertyName = "_explanation")]
		public Explanation Explanation { get; internal set; }

		[JsonProperty(PropertyName = "matched_queries")]
		public IEnumerable<string> MatchedQueries { get; internal set; }
	}
}
