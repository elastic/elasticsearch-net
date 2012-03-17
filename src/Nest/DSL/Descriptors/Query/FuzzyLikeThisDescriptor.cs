using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.DSL;
using System.Linq.Expressions;

namespace Nest
{
	public class FuzzyLikeThisDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "fields")]
		internal IEnumerable<string> _Fields { get; set; }
		[JsonProperty(PropertyName = "like_text")]
		internal string _LikeText { get; set; }
		[JsonProperty(PropertyName = "ignore_tf")]
		internal bool? _IgnoreTermFrequency { get; set; }
		[JsonProperty(PropertyName = "max_query_terms")]
		internal int? _MaxQueryTerms { get; set; }
		[JsonProperty(PropertyName = "min_similarity")]
		internal int? _MinSimilarity { get; set; }
		[JsonProperty(PropertyName = "prefix_length")]
		internal int? _PrefixLength { get; set; }
		[JsonProperty(PropertyName = "boost")]
		internal double? _Boost { get; set; }
		[JsonProperty(PropertyName = "analyzer")]
		internal string _Analyzer { get; set; }

		public FuzzyLikeThisDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			this._Fields = fields;
			return this;
		}
		public FuzzyLikeThisDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			var fieldNames = objectPaths
				.Select(o => ElasticClient.PropertyNameResolver.Resolve(o));
			return this.OnFields(fieldNames);
		}
		public FuzzyLikeThisDescriptor<T> LikeText(string likeText)
		{
			likeText.ThrowIfNullOrEmpty("likeText");
			this._LikeText = likeText;
			return this;
		}
		public FuzzyLikeThisDescriptor<T> IgnoreTermFrequency(bool ignore)
		{
			this._IgnoreTermFrequency = ignore;
			return this;
		}
		public FuzzyLikeThisDescriptor<T> MaxQueryTerms(int maxQueryTerms)
		{
			this._MaxQueryTerms = maxQueryTerms;
			return this;
		}
		public FuzzyLikeThisDescriptor<T> PrefixLength(int prefixLength)
		{
			this._PrefixLength = prefixLength;
			return this;
		}
		public FuzzyLikeThisDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}
		public FuzzyLikeThisDescriptor<T> Analyzer(string analyzer)
		{
			this._Analyzer = analyzer;
			return this;
		}
	
	}
}
