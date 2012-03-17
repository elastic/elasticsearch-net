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
	public class MoreLikeThisDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "fields")]
		internal IEnumerable<string> _Fields { get; set; }
		[JsonProperty(PropertyName = "like_text")]
		internal string _LikeText { get; set; }

		[JsonProperty(PropertyName = "percent_terms_to_match")]
		internal double? _TermMatchPercentage { get; set; }
		[JsonProperty(PropertyName = "stop_words")]
		internal IEnumerable<string> _StopWords { get; set; }
		[JsonProperty(PropertyName = "min_term_freq")]
		internal int? _MinTermFrequency { get; set; }
		[JsonProperty(PropertyName = "max_query_terms")]
		internal int? _MaxQueryTerms { get; set; }
		[JsonProperty(PropertyName = "min_doc_freq")]
		internal int? _MinDocumentFrequency { get; set; }
		[JsonProperty(PropertyName = "max_doc_freq")]
		internal int? _MaxDocumentFrequency { get; set; }
		[JsonProperty(PropertyName = "min_word_len")]
		internal int? _MinWordLength { get; set; }
		[JsonProperty(PropertyName = "max_word_len")]
		internal int? _MaxWordLength { get; set; }
		[JsonProperty(PropertyName = "boost_terms")]
		internal double? _BoostTerms { get; set; }
		[JsonProperty(PropertyName = "boost")]
		internal double? _Boost { get; set; }
		[JsonProperty(PropertyName = "analyzer")]
		internal string _Analyzer { get; set; }

		public MoreLikeThisDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			this._Fields = fields;
			return this;
		}
		public MoreLikeThisDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			var fieldNames = objectPaths
				.Select(o => ElasticClient.PropertyNameResolver.Resolve(o));
			return this.OnFields(fieldNames);
		}
		public MoreLikeThisDescriptor<T> LikeText(string likeText)
		{
			likeText.ThrowIfNullOrEmpty("likeText");
			this._LikeText = likeText;
			return this;
		}
		public MoreLikeThisDescriptor<T> StopWords(IEnumerable<string> stopWords)
		{
			this._StopWords = stopWords;
			return this;
		}
		
		public MoreLikeThisDescriptor<T> MaxQueryTerms(int maxQueryTerms)
		{
			this._MaxQueryTerms = maxQueryTerms;
			return this;
		}
		public MoreLikeThisDescriptor<T> MinTermFrequency(int minTermFrequency)
		{
			this._MinTermFrequency = minTermFrequency;
			return this;
		}
		public MoreLikeThisDescriptor<T> MinDocumentFrequency(int minDocumentFrequency)
		{
			this._MinDocumentFrequency = minDocumentFrequency;
			return this;
		}
		public MoreLikeThisDescriptor<T> MaxDocumentFrequency(int maxDocumentFrequency)
		{
			this._MaxDocumentFrequency = maxDocumentFrequency;
			return this;
		}
		public MoreLikeThisDescriptor<T> MinWordLength(int minWordLength)
		{
			this._MinWordLength = minWordLength;
			return this;
		}
		public MoreLikeThisDescriptor<T> MaxWordLength(int maxWordLength)
		{
			this._MaxWordLength = maxWordLength;
			return this;
		}
		public MoreLikeThisDescriptor<T> BoostTerms(double boostTerms)
		{
			this._BoostTerms = boostTerms;
			return this;
		}
		public MoreLikeThisDescriptor<T> TermMatchPercentage(double termMatchPercentage)
		{
			this._TermMatchPercentage = termMatchPercentage;
			return this;
		}
		public MoreLikeThisDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}
		public MoreLikeThisDescriptor<T> Analyzer(string analyzer)
		{
			this._Analyzer = analyzer;
			return this;
		}
	
	}
}
